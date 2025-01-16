﻿using Hydra.Crm.Core.Models;
using Hydra.Infrastructure.GeneralModels;


namespace Hydra.Crm.Core.Interfaces
{
    public interface IMessageSettingsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Result<MessageSettingModel> GetSettings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<MessageSettingModel> AddOrUpdate(MessageSettingModel siteSettingsModel);

    }
}
