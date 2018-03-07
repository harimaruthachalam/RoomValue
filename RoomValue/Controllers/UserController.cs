using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Web.Security;
using System.Web.Routing;
using RoomValue.Models;
using System.Text;

namespace RoomValue.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                return View();
            }
        }
        public ActionResult List()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    return View();
                }
            }
        }
        public String Data()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return "";
            else
            {
                StringBuilder stringJSON = new StringBuilder();
                stringJSON.Append("{\"List\":[");
                RoomValue.Models.ExpenditureForPersonModel[] list = RoomValue.Models.UserModel.GetExpenditureDetails().ToArray();
                bool firstTime = true;
                int count = 1;
                foreach (RoomValue.Models.ExpenditureForPersonModel expenditure in list)
                {
                    if (!firstTime)
                    {
                        stringJSON.Append(",");

                    }
                    else
                        firstTime = false;
                    stringJSON.Append("{");
                    stringJSON.Append("\"No\":" + count + ", ");
                    stringJSON.Append("\"Date\":\"" + expenditure.Date + "\", ");
                    stringJSON.Append("\"Item\":\"" + expenditure.Item + "\", ");
                    stringJSON.Append("\"Amount\":\"" + expenditure.TotalAmount + "\", ");
                    stringJSON.Append("\"PaidTo\":\"" + expenditure.PaidTo + "\", ");
                    stringJSON.Append("\"PayType\":\"" + expenditure.PayType + "\", ");
                    stringJSON.Append("\"Status\":\"" + expenditure.Status + "\", ");
                    stringJSON.Append("\"BalanceAmount\":\"" + expenditure.BalanceAmount + "\", ");
                    stringJSON.Append("\"PaidBy\":\"" + expenditure.PaidBy + "\"}");
                    count++;
                }
                stringJSON.Append("]}");
                return stringJSON.ToString();
            }
        }
        [HttpGet]
        public ActionResult Expenditure()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    Member[] members = RoomValue.Models.AccountMembershipService.GetMembers();
                    ViewData["Members"] = members;
                    return View();
                }
            }
        }
        [HttpPost]
        [ActionName("Expenditure")]
        public ActionResult MyExpenditure(String tbItem, String tbTotalAmount, String rbMemberSelection, List<String> Check)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    decimal perHead;
                    String payType;
                    if (rbMemberSelection.Equals("SomeWithMe"))
                    {
                        if (Check == null)
                        {
                            List<String> list = new List<string>();
                            list.Add(System.Web.HttpContext.Current.Session["UserName"].ToString());
                            Check = list;
                        }
                        else
                            Check.Add(System.Web.HttpContext.Current.Session["UserName"].ToString());
                        perHead = decimal.Parse(tbTotalAmount) / Check.Count;
                        payType = "WMe";
                    }
                    else if (rbMemberSelection.Equals("All"))
                    {
                        Check = UserModel.GetAllMembers();
                        perHead = decimal.Parse(tbTotalAmount) / UserModel.GetMembersCount();
                        payType = "All";
                    }
                    else
                    {
                        perHead = decimal.Parse(tbTotalAmount) / Check.Count;
                        payType = "WOMe";
                    }
                    UserModel.ExpenditureMade(tbItem, perHead, Check, payType);
                    return RedirectToAction("ExpenDone", "User");
                }
            }
        }
        public ActionResult ExpenDone()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                    return View();
            }
        }
        public ActionResult HaveToPay()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    Member[] members = UserModel.GetMembersIHaveToPay().ToArray();
                    ViewData["Members"] = members;
                    return View();
                }
            }
        }
        public String GetPayForListXML(String ID)
        {
            StringBuilder stringXML = new StringBuilder();
            stringXML.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return stringXML.ToString();
            else
            {
                stringXML.Append("<List>");
                HaveToPayModel[] haveToPay = UserModel.GetHaveToPayList(ID).ToArray();
                foreach (HaveToPayModel tempHaveToPay in haveToPay)
                {
                    stringXML.Append("<HaveToPay>");
                    stringXML.Append("<ExpenditureID>" + tempHaveToPay.ExpenditureID + "</ExpenditureID>");
                    stringXML.Append("<Item>" + tempHaveToPay.Item + "</Item>");
                    stringXML.Append("<Amount>" + tempHaveToPay.Amount + "</Amount>");
                    stringXML.Append("</HaveToPay>");
                }
                stringXML.Append("</List>");
                return stringXML.ToString();
            }
        }

        [HttpPost]
        [ActionName("HaveToPay")]
        public ActionResult PostHaveToPay(String dListTo, String dListFor, String tbAmount)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    if (dListFor == "Combined")
                    {
                        if (UserModel.CombinedPayment(dListTo))
                            return RedirectToAction("PaidNow", "User");
                    }
                    else
                    {
                        if (UserModel.SinglePayment(dListFor, tbAmount))
                            return RedirectToAction("PaidNow", "User");
                    }
                    return RedirectToAction("PaidNow", "User");
                }
            }
        }

        public ActionResult PaidNow()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    return View();
                }
            }
        }
        public ActionResult GotPaid()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    GotPaidModel[] GotPaidList = UserModel.GetGotPaidList().ToArray();
                    ViewData["List"] = GotPaidList;
                    return View();
                }
            }
        }
        public ActionResult Accept(int id)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    int result = UserModel.AcceptApproval(id.ToString());
                    if (result == 1)
                    {
                        ViewData["Message"] = "Record has been successfully approved!";
                    }
                    else if (result == 0)
                    {
                        ViewData["Message"] = "You are not Authorized to Approve this Record!";
                    }
                    else
                    {
                        ViewData["Message"] = "Something went wrong, try again later!";
                    }
                    return View();
                }
            }
        }
        public ActionResult Reject(int id)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    int result = UserModel.RejectApproval(id.ToString());
                    if (result == 1)
                    {
                        ViewData["Message"] = "Record has been successfully rejected!";
                    }
                    else if (result == 0)
                    {
                        ViewData["Message"] = "You are not Authorized to reject this Record!";
                    }
                    else
                    {
                        ViewData["Message"] = "Something went wrong, try again later!";
                    }
                    return View();
                }
            }
        }
        public ActionResult UpdateDetails()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    RoomValue.Models.DetailsModel details = UserModel.GetUserDetails();
                    ViewData["Details"] = details;
                    return View();
                }
            }
        }
        [HttpPost]
        [ActionName("UpdateDetails")]
        public ActionResult UpdateDetailsPost(String tbPhone, String tbEmail)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    int result;
                    if (UserModel.UpdateUserDetails(tbEmail, tbPhone))
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                    return RedirectToAction("UpdateDetailsDone/" + result, "user");
                }
            }
        }
        public ActionResult UpdateDetailsDone(int id)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "Admin");
                else
                {
                    if (id == 1)
                        ViewData["Message"] = "Details Updated Successfully!";
                    else
                        ViewData["Message"] = "Error occured while updating details! Try again later!";
                    return View();
                }
            }
        }
    }
}
