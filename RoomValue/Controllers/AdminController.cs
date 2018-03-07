using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace RoomValue.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    return View();
                }
            }

        }
        public ActionResult AddMember()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    return View();
                }
            }

        }
        [HttpPost]
        [ActionName("AddMember")]
        public ActionResult AddMemberDone(String tbName, String tbMail, String tbPhone)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    Models.MemberModel newMember = new Models.MemberModel();
                    newMember.Name = System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(tbName);
                    newMember.Mail = tbMail;
                    newMember.Phone = tbPhone;
                    newMember.ID = RoomValue.Models.AdminModel.AddMember(newMember);
                    return RedirectToAction("AddMemberPost/" + newMember.ID, "Admin");
                }
            }

        }
        public ActionResult AddMemberPost(int id)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    ViewData["ID"] = id;
                    return View();
                }
            }

        }
        public ActionResult ViewMemberDetails()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    return View();
                }
            }

        }
        public ActionResult RemoveMember()
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    RoomValue.Models.MemberModel[] members = RoomValue.Models.AdminModel.GetAllMembersDetails().ToArray();
                    ViewData["Members"] = members;
                    return View();
                }
            }

        }
        public ActionResult Remove(int id)
        {
            if (System.Web.HttpContext.Current.Session["UserName"] == null)
                return RedirectToAction("logon", "account");
            else
            {
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return RedirectToAction("Index", "user");
                else
                {
                    if (RoomValue.Models.AdminModel.RemoveMember(id) == true)
                        ViewData["Message"] = "Member removed Successfully!";
                    else
                        ViewData["Message"] = "Pending Transaction Exist! Member can't be removed!";
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
                if (!System.Web.HttpContext.Current.Session["UserName"].ToString().Equals("Admin"))
                    return "";
                else
                {
                    StringBuilder stringJSON = new StringBuilder();
                    stringJSON.Append("{\"Members\":[");
                    RoomValue.Models.MemberModel[] list = RoomValue.Models.AdminModel.GetAllMembersFullDetails().ToArray();
                    bool firstTime = true;
                    foreach (RoomValue.Models.MemberModel member in list)
                    {
                        if (!firstTime)
                        {
                            stringJSON.Append(",");

                        }
                        else
                            firstTime = false;
                        stringJSON.Append("{");
                        stringJSON.Append("\"Name\":\"" + member.Name + "\", ");
                        stringJSON.Append("\"ID\":" + member.ID + ", ");
                        stringJSON.Append("\"Phone\":\"" + member.Phone + "\", ");
                        stringJSON.Append("\"Mail\":\"" + member.Mail + "\"}");
                    }
                    stringJSON.Append("]}");
                    return stringJSON.ToString();
                }

            }
        }
    }
    }


