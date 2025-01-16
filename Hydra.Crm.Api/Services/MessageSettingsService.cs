﻿using Hydra.Crm.Core.Interfaces;
using Hydra.Crm.Core.Models;
using Hydra.Infrastructure.GeneralModels;
using Hydra.Infrastructure.Setting.Domain;
using Hydra.Infrastructure.Setting.Enum;
using Hydra.Infrastructure.Setting.Service;

namespace Hydra.Crm.Api.Services
{
    public class MessageSettingsService : IMessageSettingsService
    {
        private readonly ISettingService _settingService;


        public MessageSettingsService(ISettingService settingService)
        {
            _settingService = settingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public Result<MessageSettingModel> GetSettings()
        {
            var result = new Result<MessageSettingModel>();
            var setting = new MessageSettingModel();

            var recipientIdsForContactMessageValue = _settingService.GetByKey(key: nameof(setting.RecipientIdsForContactMessage))?.Value ?? null;
            var recipientIdsForRequestMessage = _settingService.GetByKey(key: nameof(setting.RecipientIdsForRequestMessage))?.Value ?? null;

            setting.RecipientIdsForContactMessage = !string.IsNullOrEmpty(recipientIdsForContactMessageValue) ? recipientIdsForContactMessageValue.Split(',').Select(int.Parse).ToArray() : Array.Empty<int>();
            setting.RecipientIdsForRequestMessage = !string.IsNullOrEmpty(recipientIdsForRequestMessage) ? recipientIdsForRequestMessage.Split(',').Select(int.Parse).ToArray() : Array.Empty<int>();


            result.Data = setting;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageSettingModel"></param>
        /// <returns></returns>
        public Result<MessageSettingModel> AddOrUpdate(MessageSettingModel messageSettingModel)
        {
            var result = new Result<MessageSettingModel>();
            try
            {
                _settingService.AddOrUpdate(new SiteSetting()
                {
                    Key = nameof(messageSettingModel.RecipientIdsForContactMessage),
                    Value = messageSettingModel.RecipientIdsForContactMessage.Length > 0 ? string.Join(",", messageSettingModel.RecipientIdsForContactMessage) : null,
                    ValueType = SettingValueTypeEnum.IntegerArray
                });
                _settingService.AddOrUpdate(new SiteSetting()
                {
                    Key = nameof(messageSettingModel.RecipientIdsForRequestMessage),
                    Value = messageSettingModel.RecipientIdsForRequestMessage.Length > 0 ? string.Join(",", messageSettingModel.RecipientIdsForRequestMessage) : null,
                    ValueType = SettingValueTypeEnum.IntegerArray
                });

                result.Data = messageSettingModel;

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

    }
}