﻿#region zh-CHS 2006 - 2010 DemoSoft 团队 | en 2006-2010 DemoSoft Team

//     NOTES
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

#region zh-CHS 包含名字空间 | en Include namespace
using System;
using System.Text;
using DogSE.Library.Util;
using DogSE.Server.Net;

#endregion

namespace DogSE.Server.Core.Net
{
    /// <summary>
    /// Provides functionality for writing primitive binary data.
    /// </summary>
    public class PacketWriter:IDisposable
    {
        private DogBuffer32K buffer;

        #region zh-CHS 私有成员变量 | en Private Member Variables

        /// <summary>
        /// float转换器
        /// </summary>
        private CONVERT_FLOAT_INT_UINT m_ConvertFloat;

        /// <summary>
        /// double转换器
        /// </summary>
        private CONVERT_DOUBLE_LONG_ULONG m_ConvertDouble;

        #endregion

        #region zh-CHS 构造和初始化和清理 | en Constructors and Initializers and Dispose
        /// <summary>
        /// 数据包写入器
        /// </summary>
        /// <param name="codeId">消息报的id</param>
        public PacketWriter(ushort codeId)
        {
            buffer = DogBuffer.GetFromPool32K();
            //  先预留2位用于存放消息id
            buffer.Length = 2;
            Write(codeId);
        }

        #endregion

        #region zh-CHS 属性 | en Properties
        /// <summary>
        /// Gets the total stream length.
        /// </summary>
        public long Length
        {
            get { return buffer.Length; }
        }

        #region zh-CHS 私有成员变量 | en Private Member Variables
        /// <summary>
        /// 
        /// </summary>
        private Endian m_Endian = Endian.LITTLE_ENDIAN;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Endian Endian
        {
            get { return m_Endian; }
            set { m_Endian = value; }
        }
        #endregion

        /// <summary>
        /// 判断并调整缓冲区的大小
        /// </summary>
        /// <param name="size"></param>
        void FixBuffer(int size)
        {
            if(buffer.Length + size >= buffer.Bytes.Length)
            {
                buffer.UpdateCapacity();
            }
        }

        #region zh-CHS 方法 | en Method
        /// <summary>
        /// Writes a 1-byte boolean value to the underlying stream. False is represented by 0, true by 1.
        /// </summary>
        public void Write( bool bValue )
        {
            FixBuffer(1);
            buffer.Bytes[buffer.Length++] = (byte) (bValue ? 1 : 0);
        }

        /// <summary>
        /// Writes a 1-byte unsigned integer value to the underlying stream.
        /// </summary>
        public void Write( byte byteValue )
        {
            FixBuffer(1);
            buffer.Bytes[buffer.Length++] = byteValue;
        }

        /// <summary>
        /// Writes a 1-byte signed integer value to the underlying stream.
        /// </summary>
        public void Write( sbyte sbyteValue )
        {
            FixBuffer(1);
            buffer.Bytes[buffer.Length++] = (byte)sbyteValue;
        }

        /// <summary>
        /// Writes a 2-byte signed integer value to the underlying stream.
        /// </summary>
        public void Write( short shortValue )
        {
            FixBuffer(2);
            if ( m_Endian == Endian.LITTLE_ENDIAN )
            {
                buffer.Bytes[buffer.Length++] = (byte)shortValue;
                buffer.Bytes[buffer.Length++] = (byte)(shortValue >> 8);
            }
            else
            {
                buffer.Bytes[buffer.Length++] = (byte)(shortValue >> 8);
                buffer.Bytes[buffer.Length++] = (byte)shortValue;
            }
        }

        /// <summary>
        /// Writes a 2-byte unsigned integer value to the underlying stream.
        /// </summary>
        public void Write( ushort ushortValue )
        {
            FixBuffer(2);
            if ( m_Endian == Endian.LITTLE_ENDIAN )
            {
                buffer.Bytes[buffer.Length++] = (byte)ushortValue;
                buffer.Bytes[buffer.Length++] = (byte)(ushortValue >> 8);
            }
            else
            {
                buffer.Bytes[buffer.Length++] = (byte)(ushortValue >> 8);
                buffer.Bytes[buffer.Length++] = (byte)ushortValue;
            }
        }

        /// <summary>
        /// Writes a 4-byte signed integer value to the underlying stream.
        /// </summary>
        public void Write( int intValue )
        {
            FixBuffer(4);
            if ( m_Endian == Endian.LITTLE_ENDIAN )
            {
                buffer.Bytes[buffer.Length++] = (byte)intValue;
                buffer.Bytes[buffer.Length++] = (byte)(intValue >> 8);
                buffer.Bytes[buffer.Length++] = (byte)(intValue >> 16);
                buffer.Bytes[buffer.Length++] = (byte)(intValue >> 24);
            }
            else
            {
                buffer.Bytes[buffer.Length++] = (byte)(intValue >> 24);
                buffer.Bytes[buffer.Length++] = (byte)(intValue >> 16);
                buffer.Bytes[buffer.Length++] = (byte)(intValue >> 8);
                buffer.Bytes[buffer.Length++] = (byte)intValue;
            }
        }

        /// <summary>
        /// Writes a 4-byte unsigned integer value to the underlying stream.
        /// </summary>
        public void Write( uint uintValue )
        {
            FixBuffer(4);
            if ( m_Endian == Endian.LITTLE_ENDIAN )
            {
                buffer.Bytes[buffer.Length++]  = (byte)uintValue;
                buffer.Bytes[buffer.Length++]  = (byte)( uintValue >> 8 );
                buffer.Bytes[buffer.Length++]  = (byte)( uintValue >> 16 );
                buffer.Bytes[buffer.Length++]  = (byte)( uintValue >> 24 );
            }
            else
            {
                buffer.Bytes[buffer.Length++]  = (byte)( uintValue >> 24 );
                buffer.Bytes[buffer.Length++]  = (byte)( uintValue >> 16 );
                buffer.Bytes[buffer.Length++]  = (byte)( uintValue >> 8 );
                buffer.Bytes[buffer.Length++]  = (byte)uintValue;
            }

        }

        /// <summary>
        /// Writes a 4-byte float value to the underlying stream.
        /// </summary>
        public void Write( float floatValue )
        {
            FixBuffer(4);
            m_ConvertFloat.fFloat = floatValue;
            Write( m_ConvertFloat.uiUint );
        }

        /// <summary>
        /// Writes a 8-byte long value to the underlying stream.
        /// </summary>
        public void Write( long longValue )
        {
            FixBuffer(8);
            if ( m_Endian == Endian.LITTLE_ENDIAN )
            {
                buffer.Bytes[buffer.Length++]  = (byte)longValue;
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 8 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 16 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 24 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 32 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 40 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 48 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 56 );
            }
            else
            {
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 56 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 48 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 40 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 32 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 24 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 16 );
                buffer.Bytes[buffer.Length++]  = (byte)( longValue >> 8 );
                buffer.Bytes[buffer.Length++]  = (byte)longValue;
            }

        }

        /// <summary>
        /// Writes a 8-byte unsigned long value to the underlying stream.
        /// </summary>
        public void Write( ulong ulongValue )
        {
            FixBuffer(8);
            if ( m_Endian == Endian.LITTLE_ENDIAN )
            {
                buffer.Bytes[buffer.Length++]  = (byte)ulongValue;
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 8 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 16 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 24 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 32 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 40 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 48 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 56 );
            }
            else
            {
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 56 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 48 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 40 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 32 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 24 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 16 );
                buffer.Bytes[buffer.Length++]  = (byte)( ulongValue >> 8 );
                buffer.Bytes[buffer.Length++]  = (byte)ulongValue;
            }
        }

        /// <summary>
        /// Writes a 8-byte double value to the underlying stream.
        /// </summary>
        public void Write( double doubleValue )
        {
            FixBuffer(8);
            m_ConvertDouble.dDouble = doubleValue;
            Write( m_ConvertDouble.ulUlong );
        }

        /// <summary>
        /// Writes a sequence of bytes to the underlying stream
        /// </summary>
        public void Write( byte[] byteBuffer, int iOffset, int iSize )
        {
            FixBuffer(iSize);
            Buffer.BlockCopy(byteBuffer, iOffset, buffer.Bytes, buffer.Length, iSize);
            buffer.Length += iSize;
        }

        /// <summary>
        /// Writes a dynamic-length ASCII-encoded string value to the underlying stream, followed by a 1-byte null character.
        /// </summary>
        /// <param name="strValue"></param>
        public void WriteAsciiNull( string strValue )
        {
            if ( strValue == null )
                strValue = string.Empty;

            byte[] strBuffer = Encoding.ASCII.GetBytes( strValue );

            FixBuffer(strBuffer.Length);

            Write( strBuffer, 0, strBuffer.Length );
            Write((byte)0);
        }

        /// <summary>
        /// Writes a dynamic-length little-endian unicode string value to the underlying stream, followed by a 2-byte null character.
        /// </summary>
        /// <param name="strValue"></param>
        public void WriteLittleUniNull( string strValue )
        {
            if ( strValue == null )
                strValue = string.Empty;

            byte[] strBuffer = Encoding.Unicode.GetBytes(strValue);

            FixBuffer(strBuffer.Length);

            Write(strBuffer, 0, strBuffer.Length);
            Write((short) 0);   //补一个双字节的\0结束符
        }

        /// <summary>
        /// Writes a dynamic-length big-endian unicode string value to the underlying stream, followed by a 2-byte null character.
        /// </summary>
        /// <param name="strValue"></param>
        public void WriteBigUniNull( string strValue )
        {
            if ( strValue == null )
                strValue = string.Empty;

            byte[] strBuffer = Encoding.BigEndianUnicode.GetBytes(strValue);

            FixBuffer(strBuffer.Length);

            Write(strBuffer, 0, strBuffer.Length);
            Write((short)0);   //补一个双字节的\0结束符
        }

        /// <summary>
        /// Writes a dynamic-length UTF8 string value to the underlying stream, followed by a 2-byte null character.
        /// </summary>
        /// <param name="strValue"></param>
        public void WriteUTF8Null( string strValue )
        {
            if (strValue == null)
                strValue = string.Empty;

            byte[] strBuffer = Encoding.UTF8.GetBytes( strValue );

            FixBuffer(strBuffer.Length);

            Write(strBuffer, 0, strBuffer.Length);
            Write((byte)0);   //补一个字节的\0结束符
        }


        /// <summary>
        /// 获得缓冲区数据
        /// 在获得的时候，会根据当前缓冲区的长度，把长度编入首字节的4位
        /// </summary>
        /// <returns></returns>
        public DogBuffer GetBuffer()
        {
            var intValue = buffer.Length;

            if (m_Endian == Endian.LITTLE_ENDIAN)
            {
                buffer.Bytes[0] = (byte)intValue;
                buffer.Bytes[1] = (byte)(intValue >> 8);
                buffer.Bytes[2] = (byte)(intValue >> 16);
                buffer.Bytes[3] = (byte)(intValue >> 24);
            }
            else
            {
                buffer.Bytes[0] = (byte)(intValue >> 24);
                buffer.Bytes[1] = (byte)(intValue >> 16);
                buffer.Bytes[2] = (byte)(intValue >> 8);
                buffer.Bytes[3] = (byte)intValue;
            }
            return buffer;
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose(bool t)
        {
            if (!t)
                return;

            if (buffer != null)
            {
                buffer.Release();
                buffer = null;
            }   
        }

        #endregion

        #region IDisposable 成员

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
#endregion

