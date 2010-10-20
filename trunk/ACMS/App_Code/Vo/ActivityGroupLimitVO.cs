using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class ActivityGroupLimitVO : BaseVO
    {
        private int _id;      
        private Guid _activity_id;
        private string _emp_id;

        public int id { get { return _id; } set { _id = value; } }
        public Guid activity_id { get { return _activity_id; } set { _activity_id = value; } }
        public string emp_id { get { return _emp_id; } set { _emp_id = value; } }

    }


}
