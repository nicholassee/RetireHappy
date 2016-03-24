using NPOI.SS.UserModel;
using RetireHappy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace RetireHappy.DAL
{
    public class AvgExpenditureGateway : CommonGateway<AvgExpenditure>
    {
        public void deleteAll()
        {
            string query = "DELETE FROM AvgExpenditure";
            db.Database.ExecuteSqlCommand(query);
        }

        public void UploadDataset(HttpPostedFileBase uploadFile)
        {
            //reference from https://zenpad.wordpress.com/2015/01/22/reading-or-writing-excel-files-using-c/
            string path  = HostingEnvironment.MapPath("~/Content/" + uploadFile.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            uploadFile.SaveAs(path);
            IWorkbook wb = WorkbookFactory.Create(new FileStream(
               Path.GetFullPath(path),
               FileMode.Open, FileAccess.Read,
               FileShare.ReadWrite));
            ISheet ws = wb.GetSheetAt(3);
            string tempCategory = "";
            AvgExpenditure avgExpenditure = new AvgExpenditure();
            int checkResult;
            // avgExpenditureGateway.deleteAll();
            // assuming format is fixed, row starts from 9 and column for category and type is in 0 and figure is in column 1
            for (int row = 8; row <= ws.LastRowNum; row++)
            {
                string temp = ws.GetRow(row).GetCell(0).ToString();
                if (string.IsNullOrEmpty(temp))
                {
                    // not needed
                }
                else
                {
                    //According to format of dataset
                    if (!char.IsWhiteSpace(temp[3]))
                    {
                        tempCategory = temp;
                    }
                    //According to format of dataset
                    else if (temp.Length > 4)
                    {
                        if (char.IsWhiteSpace(temp[3]) == true && char.IsWhiteSpace(temp[4]) == false && ws.GetRow(row).GetCell(1).ToString() != "-")
                        {
                            //AvgExpenditure avgExpenditure = new AvgExpenditure();
                            avgExpenditure.type = temp.Trim();
                            avgExpenditure.category = tempCategory.Trim();
                            avgExpenditure.recordYear = System.DateTime.Now;
                            avgExpenditure.avgAmount = double.Parse(ws.GetRow(row).GetCell(1).ToString());
                            checkResult = CheckIfExist(avgExpenditure.category, avgExpenditure.type);
                            // CheckIfExist returns 0 if row does not exist and return eId of record if exist
                            if (checkResult != 0)
                            {
                                // if record exist, update row with same eId
                                avgExpenditure.eId = checkResult;
                                UpdateRow(avgExpenditure);
                            }
                            else
                            {
                                Insert(avgExpenditure);
                            }
                        }
                    }
                }

            }
        }

        public int CheckIfExist(string category, string type)
        {
            AvgExpenditure avgExpenditure = new AvgExpenditure();
            string query = "SELECT * FROM AvgExpenditure WHERE category LIKE {0} AND type LIKE {1}";
            try
            {
                avgExpenditure = db.AvgExpenditures.SqlQuery(query, category, type).Single();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return avgExpenditure.eId;
        }
        
        public void UpdateRow(AvgExpenditure avgExpenditure)
        {
            string query = "UPDATE AvgExpenditure SET category = {0}, type = {1}, recordYear = {2}, avgAmount = {3} WHERE eId = {4}";
            db.Database.ExecuteSqlCommand(query, avgExpenditure.category, avgExpenditure.type, avgExpenditure.recordYear, avgExpenditure.avgAmount, avgExpenditure.eId);
            Save();
        }

        public IEnumerable<AvgExpenditure> MatchExpenditureList(int mId)
        {
            string query = "SELECT * FROM AvgExpenditure A WHERE EXISTS " +
            "(SELECT * FROM ExpenditureList E WHERE E.mId = {0} " +
            "AND(A.eId = E.item1 OR A.eId = E.item2 OR A.eId = E.item3 OR A.eId = E.item4 OR A.eId = E.item5 " +
            "OR A.eId = E.item6 OR A.eId = E.item7 OR A.eId = E.item8 OR A.eId = E.item9 OR A.eId = E.item10))";
            try
            {
                IEnumerable<AvgExpenditure> avgExpenditure = db.Database.SqlQuery<AvgExpenditure>(query, mId);
                return avgExpenditure;
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return null;
        }
    }
}