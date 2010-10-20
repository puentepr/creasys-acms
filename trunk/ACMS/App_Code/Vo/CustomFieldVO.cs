using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class CustomFieldVO : BaseVO
    {
        private Guid _activity_id;
        private int _field_id;
        private string _field_name;
        private string _field_control;
        private int _isShowEdit;//SELECT時動態產生

        

        public Guid activity_id { get { return _activity_id; } set { _activity_id = value; } }
        public int field_id { get { return _field_id; } set { _field_id = value; } }
        public string field_name { get { return _field_name; } set { _field_name = value; } }
        public string field_control { get { return _field_control; } set { _field_control = value; } }
        public int isShowEdit { get { return _isShowEdit; } set { _isShowEdit = value; } }     


    }


}
