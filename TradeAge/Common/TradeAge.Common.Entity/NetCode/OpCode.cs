﻿namespace TradeAge.Common.Entity.NetCode
{
    /// <summary>
    /// 游戏的消息码
    /// </summary>
    public enum OpCode:ushort
    {
        #region 0 - 1000 游戏系统保留消息码
        
        /// <summary>
        /// 心跳包
        /// </summary>
        Heart = 0,

        /// <summary>
        /// 服务器时间
        /// </summary>
        ServerTime = 1,

        #endregion

        #region 1000  - 1999 登陆相关消息码
        
        /// <summary>
        /// 客户端登陆服务器
        /// </summary>
        LoginServer = 1000,

        #endregion
    }
}
