using SwinSchool.CommonShared;
using SwinSchool.CommonShared.Dto;
using SwinSchool.DAL.DAO;
using SwinSchool.DAL.DAO.Impl;
using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Web;

namespace SwinSchool.BusinessLogicServer
{
    /// <summary>
    /// This class listen to a Message Queue to do service related to user account. 
    /// There should be only one instance of this service running
    /// Get singleton instance : UserMaintenanceService.Instance
    /// </summary>
    public class UserMaintenanceService
    {
        const string QUEUE_NAME = @".\private$\UserAccountQueue";

        private IMyUserDao _myUserDao;
        private EmailService _emailService;

        private static UserMaintenanceService _singleton;
        public static UserMaintenanceService Instance
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new UserMaintenanceService();
                }

                return _singleton;
            }
        }

        private UserMaintenanceService()
        {
            // check for queue existence
            if (!MessageQueue.Exists(QUEUE_NAME))
            {
                // create a transaction queue if not exist
                MessageQueue.Create(QUEUE_NAME, true);
            }

            //Connect to the queue
            MessageQueue Queue = new MessageQueue(QUEUE_NAME);

            Queue.ReceiveCompleted += new ReceiveCompletedEventHandler(ProcessMessage);
            Queue.BeginReceive();

            // initialize required dependencies
            _myUserDao = new MyUserDao();
            _emailService = new EmailService();

        }

        private void ProcessMessage(object source, ReceiveCompletedEventArgs asyncResult)
        {
            // Connect to the queue.
            MessageQueue Queue = (MessageQueue)source;
            // End the asynchronous receive operation.
            System.Messaging.Message msg = Queue.EndReceive(asyncResult.AsyncResult);
            
            // process raw message
            msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(ResetPasswordRequestDto) });
            ResetPasswordRequestDto request = (ResetPasswordRequestDto)msg.Body;
            
            // process reset password logic
            try
            {
                var user = doResetPassword(request.UserId);
                _emailService.SendUpdatedPasswordEmail(user);
                AppLogger.Log("Email sent for user " + user.Name + " with password: " + user.Password);
            }
            catch (Exception ex)
            {
                AppLogger.Log("Cannot reset password using Messaging service", ex);
            }
            
            // continue listening on the queue for the next message
            Debug.WriteLine("Processing {0} ", request);
            Queue.BeginReceive();
        }

        public MyUser doResetPassword(string userId)
        {
            var user = _myUserDao.GetById(userId);

            // Perform generating new password.
            user.Password = RandomManager.GenerateRandomString(8);

            // save updated entity to the database
            if (_myUserDao.Update(user) <= 0)
            {
                throw new Exception("Database error: User password cannot be updated");
            }

            // log the updated password into a server log file
            AppLogger.Info(string.Format("Password has been updated for user : {0} to : {1}", user.Name, user.Password));
            return user;
        }
    }
}