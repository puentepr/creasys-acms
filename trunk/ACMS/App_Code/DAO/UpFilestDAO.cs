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

        /// <summary>
        /// 取得附加檔案資料
        /// </summary>
        /// <param name="dirName">附加檔案目錄</param>
        /// <returns>取得附加檔案資料</returns>
        public List<VO.UpFileVO> SELECT(string dirName)
        {
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(dirName);

            List<VO.UpFileVO> myUpFileVOList = new List<ACMS.VO.UpFileVO>();

            if (myDirectoryInfo.Exists)
            {

                foreach (FileInfo myFileInfo in myDirectoryInfo.GetFiles())
                {
                    VO.UpFileVO myUpFileVO = new ACMS.VO.UpFileVO();
                    myUpFileVO.name = Path.GetFileName(myFileInfo.FullName);
                    myUpFileVO.path = myFileInfo.FullName;
                    myUpFileVOList.Add(myUpFileVO);
                }
            }

            return myUpFileVOList;


        }

    }
}
