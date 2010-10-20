using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class CustomFieldValueVO : BaseVO
    {
        private Guid _id;
        private string _emp_id;
        private int _field_id;
        private string _field_value;

        public Guid id { get { return _id; } set { _id = value; } }
        public string emp_id { get { return _emp_id; } set { _emp_id = value; } }
        public int field_id { get { return _field_id; } set { _field_id = value; } }
        public string field_value { get { return _field_value; } set { _field_value = value; } }

    }

}
