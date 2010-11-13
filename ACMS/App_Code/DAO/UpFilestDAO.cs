using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class UpFilestDAO : BaseDAO
    {
        public List<VO.UpFileVO> SELECT(string dirName)
        {  
            DirectoryInfo myDirectoryInfo= new DirectoryInfo(dirName);

            List<VO.UpFileVO> myUpFileVOList = new List<ACMS.VO.UpFileVO>();

            if (myDirectoryInfo.Exists)
            {

                foreach(FileInfo myFileInfo in myDirectoryInfo.GetFiles())
                {
                         VO.UpFileVO myUpFileVO = new ACMS.VO.UpFileVO();
                         myUpFileVO.name =Path.GetFileName(myFileInfo.FullName);
                         myUpFileVO.path = myFileInfo.FullName;
                         myUpFileVOList.Add(myUpFileVO);
                }             
            }

            return myUpFileVOList;
        }    

    }
}
