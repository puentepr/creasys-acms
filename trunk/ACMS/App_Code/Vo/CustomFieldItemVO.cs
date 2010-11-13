using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class CustomFieldItemVO : BaseVO
    {
        private int _field_id;
        private int _field_item_id;
        private string _field_item_name;
        private string _field_item_text;  

        public int field_id { get { return _field_id; } set { _field_id = value; } }
        public int field_item_id { get { return _field_item_id; } set { _field_item_id = value; } }

        public string field_item_name { get { return _field_item_name; } set { _field_item_name = value; } }
        public string field_item_text { get { return _field_item_text; } set { _field_item_text = value; } }

    }

}
