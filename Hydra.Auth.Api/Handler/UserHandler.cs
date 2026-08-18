using Hydra.Auth.Interface;
using Hydra.Auth.Models;
using Hydra.Kernel.GeneralModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Auth.Api.Handler
{
    public class UserHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetList(
             IUserService _userService, GridDataBound dataGrid)
        {
            var result = await _userService.GetList(dataGrid);
            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelect(
             IUserService _userService, [FromBody] string input)
        {
            var result = await _userService.GetListForSelect(input);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetListForSelectByIds(
             IUserService _userService, [FromBody] int[] userIds)
        {
            var result = await _userService.GetListForSelectByIds(userIds);

            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static async Task<IResult> GetUserById(
            IUserService _userService,
            int userId
            )
        {
            var result = await _userService.GetById(userId);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static async Task<IResult> AddUser(
            IUserService _userService,
            [FromBody] UserModel userModel
            )
        {
            var result = await _userService.Add(userModel);

            return Results.Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateUser(
            IUserService _userService,
            [FromBody] UserModel userModel
            )
        {
            var result = await _userService.Update(userModel);

            return Results.Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userService"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static async Task<IResult> DeleteUser(
            IUserService _userService,
            int userId
            )
        {
            var result = await _userService.DeleteUser(userId);

            return Results.Ok(result);
        }

    }
}
