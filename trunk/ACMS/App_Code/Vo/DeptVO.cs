using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class DDLVO : BaseVO
    {
        private string _Text;
        private string _Value;

        public string Text { get { return _Text; } set { _Text = value; } }
        public string Value { get { return _Value; } set { _Value = value; } }
 

    }

}
