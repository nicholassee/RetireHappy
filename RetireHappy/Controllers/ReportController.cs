using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using RetireHappy.DAL;
using RetireHappy.Models;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace RetireHappy.Controllers
{
    public class ReportController : Controller
    {
        private RetireHappyContext db = new RetireHappyContext();
        private ReportMapper reportMapper = new ReportMapper();

        // GET: Report
        public ActionResult Index()
        {
            Report report = new Report();
            report.updateData();
            return View(report);
        }

        // GET: Report
        public ActionResult Table()
        {
            Report report = new Report();
            report.updateData();
            return View(report);
        }

        public ActionResult ExportData()
        {
            //http://www.c-sharpcorner.com/UploadFile/rahul4_saxena/export-data-table-to-excel-in-Asp-Net-mvc-4/
            //Report report = (Report)reportMapper.retrieveInfo();
            Report report = new Report();
            report.updateData();

            DataTable dt1 = new DataTable();
            dt1.TableName = "Total Response for Both Gender";
            dt1.Columns.Add(new DataColumn("Male", typeof(string)));
            dt1.Columns.Add(new DataColumn("Female", typeof(string)));

            DataRow row = dt1.NewRow();
            row["Male"] = report.male;
            row["Female"] = report.female;
            dt1.Rows.Add(row);

            DataTable dt2 = new DataTable();

            dt2.TableName = "Avg Savings Income Range";
            dt2.Columns.Add(new DataColumn("Below 1000", typeof(string)));
            dt2.Columns.Add(new DataColumn("1001 - 2000", typeof(string)));
            dt2.Columns.Add(new DataColumn("2001 - 3000", typeof(string)));
            dt2.Columns.Add(new DataColumn("3001 - 4000", typeof(string)));
            dt2.Columns.Add(new DataColumn("4001 - 5000", typeof(string)));
            dt2.Columns.Add(new DataColumn("5001 - 6000", typeof(string)));
            dt2.Columns.Add(new DataColumn("Above 6000", typeof(string)));

            DataRow row2 = dt2.NewRow();
            row2["Below 1000"] = report.inc_below_1000;
            row2["1001 - 2000"] = report.inc_1001_2000;
            row2["2001 - 3000"] = report.inc_2001_3000;
            row2["3001 - 4000"] = report.inc_3001_4000;
            row2["4001 - 5000"] = report.inc_4001_5000;
            row2["5001 - 6000"] = report.inc_5001_6000;
            row2["Above 6000"] = report.inc_above_6000;
            dt2.Rows.Add(row2);

            DataTable dt3 = new DataTable();

            dt3.TableName = "Avg Savings Age Range";
            dt3.Columns.Add(new DataColumn("Below 25", typeof(string)));
            dt3.Columns.Add(new DataColumn("25 - 34", typeof(string)));
            dt3.Columns.Add(new DataColumn("35 - 44", typeof(string)));
            dt3.Columns.Add(new DataColumn("45 - 54", typeof(string)));
            dt3.Columns.Add(new DataColumn("55 - 64", typeof(string)));

            DataRow row3 = dt3.NewRow();
            row3["Below 25"] = report.ageRange_below_25;
            row3["25 - 34"] = report.ageRange_25_34;
            row3["35 - 44"] = report.ageRange_35_44;
            row3["45 - 54"] = report.ageRange_45_54;
            row3["55 - 64"] = report.ageRange_55_64;
            dt3.Rows.Add(row3);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt1);
                wb.Worksheets.Add(dt2);
                wb.Worksheets.Add(dt3);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= RetireHappyReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }

    }
}