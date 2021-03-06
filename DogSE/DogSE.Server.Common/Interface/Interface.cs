﻿//     NOTES
// ---------------
//
// This file is a part of the MMOSE(Massively Multiplayer Online Server Engine) for .NET.
//
//                              2006-2010 DemoSoft Team
//
//
// First Version : by H.Q.Cai - mailto:caihuanqing@hotmail.com
// Update Version: by Dogvane - mailto:dogvane@gmail.com

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU Lesser General Public License as published
 *   by the Free Software Foundation; either version 2.1 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

namespace DogSE.Server.Common
{
    #region zh-CHS 接口 | en Interface
    /// <summary>
    /// 具备唯一标示的接口
    /// </summary>
    public interface ISerial
    {
        /// <summary>
        /// 唯一标示Id
        /// </summary>
        Serial Serial { get; }
    }

    /// <summary>
    /// 用于保存数据的实体类
    /// </summary>
    public interface IDataEntity:ISerial
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPoint2D
    {
        /// <summary>
        /// 
        /// </summary>
        float X { get; }

        /// <summary>
        /// 
        /// </summary>
        float Y { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPoint3D : IPoint2D
    {
        /// <summary>
        /// 
        /// </summary>
        float Z { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IDirection
    {
        /// <summary>
        /// 
        /// </summary>
        Vector3 Direction { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ILocation : IPoint3D, IDirection
    {
        /// <summary>
        /// 
        /// </summary>
        Point3D Location { get; }
    }
    
    /// <summary>
    /// 当前脚本类需初始化实例
    /// </summary>
    public interface IInitialize
    {
    }

    /// <summary>
    /// 当前脚本类需配置化实例
    /// </summary>
    public interface IConfigure
    {
    }

    /// <summary>
    /// 当前类的有内存池,可释放自己入内存池
    /// </summary>
    public interface IRelease
    {
        #region zh-CHS 接口 | en Interface
        /// <summary>
        /// 
        /// </summary>
        void Release();
        #endregion
    }

    /// <summary>
    /// 当前类实例的在多线程中需锁定
    /// </summary>
    public interface ILock
    {
        #region zh-CHS 接口 | en Interface
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool Lock();
        /// <summary>
        /// 
        /// </summary>
        void Free();
        #endregion
    }

    #endregion
}
