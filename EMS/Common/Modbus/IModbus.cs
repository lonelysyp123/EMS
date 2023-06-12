using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Common.Modbus
{
    public interface IModbus
    {
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns>创建连接是否成功</returns>
        bool Connect();

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns>断开连接是否成功</returns>
        bool Disconnect();

        /// <summary>
        /// 读取请求
        /// </summary>
        /// <param name="ret">请求返回结果</param>
        /// <param name="address">地址</param>
        /// <param name="offset">偏移量</param>
        /// <param name="length">长度</param>
        /// <returns>读取是否成功</returns>
        bool ReadRegister(ref byte[] ret, Address address, int offset, int length);

        /// <summary>
        /// 写入请求
        /// </summary>
        /// <param name="ret">请求返回结果</param>
        /// <param name="address">地址</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">长度</param>
        /// <returns>写入是否成功</returns>
        bool WriteRegister(ref byte[] ret, Address address, int offset, byte[] value);
    }
}
