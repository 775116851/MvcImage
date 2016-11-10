using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace MvcImage.Models
{
    [DataContract]
    [Serializable]
    public class ImagesModels : IComparable<ImagesModels>
    {
        public ImagesModels()
        {
            Init();
        }

        [DataMember]
        public string ImagePic { get; set; }
        [DataMember]
        public string ImageHidPic { get; set; }

        /// <summary>
        /// 其他验证提示信息
        /// </summary>
        public int ValidateInfo { get; set; }

        [System.Runtime.Serialization.OnDeserializing]
        private void OnDeserializing(StreamingContext ctx)
        {
            Init();
        }

        public void Init()
        {
            ImagePic = "";
            ImageHidPic = "";
        }

        #region 实现IComparable<T>接口的泛型排序方法
        /// <sumary> 
        /// 根据SysNo字段实现的IComparable<T>接口的泛型排序方法 
        /// </sumary> 
        /// <param name="other"></param> 
        /// <returns></returns> 
        public int CompareTo(ImagesModels other)
        {
            return ImagePic.CompareTo(other.ImagePic);
        }
        #endregion
    }
}