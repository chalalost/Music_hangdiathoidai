using OnlineMusic.Common;
using OnlineMusic.DAO;
using OnlineMusic.EF;
using OnlineMusic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PayPal.Api;

namespace OnlineMusic.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Cart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return View(list);
        }
        public ActionResult SmallCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return PartialView(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItems>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItems>>(cartModel);
            var sessCart = (List<CartItems>)Session[CartSession];

            foreach (var item in sessCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null && jsonItem.Quantity > 0)
                {
                    item.Quantity = jsonItem.Quantity;
                }
                else
                {
                    Session[CartSession] = sessCart;
                    return Json(new
                    {
                        status = false
                    }); ;
                }
            }
            Session[CartSession] = sessCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productID, int quantity)
        {
            var product = new PRODUCT_DAO().ViewDetail(productID);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItems>)cart;
                if (list.Exists(x => x.Product.ID == productID))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItems();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                var item = new CartItems();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItems>();
                list.Add(item);

                Session[CartSession] = list;
            }
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string txtshipName, string txtmobile, string txtaddress, string txtemail)
        {
            var order = new ORDER();
            order.CreateDate = DateTime.Now.ToString();
            order.ShipAddress = txtaddress;
            order.ShipEmail = txtemail;
            order.ShipName = txtshipName;
            order.ShipPhone = txtmobile;

            try
            {
                var id = new ORDER_DAO().Insert(order);
                var cart = (List<CartItems>)Session[CartSession];
                var detailDao = new ORDERDETAIL_DAO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new ORDERDETAIL();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", txtshipName);
                content = content.Replace("{{Phone}}", txtmobile);
                content = content.Replace("{{Email}}", txtemail);
                content = content.Replace("{{Address}}", txtaddress);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(txtemail, "Đơn hàng mới từ Hãng Đĩa Thời Đại| Time Records", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Hãng Đĩa Thời Đại| Time Records", content);
            }
            catch (Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }
            Session.Remove(CartSession);
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }

        private Payment payment;

        // Create a paypment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };

            List<CartItems> listCarts = (List<CartItems>)Session[CartSession];
            foreach (var cart in listCarts)
            {
                listItems.items.Add(new Item()
                {
                    name = cart.Product.Name,
                    currency = "USD",
                    price = cart.Product.Price.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Do the configuration RedirectURLs here with redirectURLs object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = listCarts.Sum(x => x.Quantity * x.Product.Price).ToString()
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),// tax + shipping + subtotal
                details = details
            };

            // Create transaction
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Chien Testing transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }

        // Create ExecutePayment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        // Create PaymentWithPaypal method
        public ActionResult PaymentWithPaypal()
        {
            // Gettings context from the paypal bases on clientId and clientSecret for payment
            APIContext apiContext = PaypalConfig.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get links returned from paypal response to create call funciton
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        //Remove shopping cart session
                        Session.Remove(CartSession);
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                //Remove shopping cart session
                Session.Remove(CartSession);
                return View("Failure");
            }

            //Remove shopping cart session
            Session.Remove(CartSession);
            return View("SuccessPayPal");
        }
    }
}