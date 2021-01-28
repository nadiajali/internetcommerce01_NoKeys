using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace internetcommerce01.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithPayPal(string Cancel = null)
        {
            APIContext apiContext = PayPalConfiguration.GetAPIContext();

            try
            {
                string payerID = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerID))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    Console.WriteLine(baseURI);
                    Console.WriteLine(guid);
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectURL = null;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;

                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectURL = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectURL);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerID, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: ", exception);
                return View("Failure");
            }

            Session.Clear();
            return View("Success");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerID, string paymentID)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerID };
            this.payment = new Payment() { id = paymentID };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectURL)
        {
            var itemList = new ItemList() { items = new List<Item>() };

            if (Session["cart"] != null)
            {
                List<Models.Home.Item> cart = (List<Models.Home.Item>)(Session["cart"]);

                foreach (var item in cart)
                {
                    itemList.items.Add(new Item()
                    {
                        name = Convert.ToString(item.Product.ProductName),
                        currency = "USD",
                        price = Convert.ToString(Convert.ToDecimal(item.Product.Price)),
                        // price = "1",
                        quantity = Convert.ToString(item.Quantity),
                        // quantity = "1",
                        sku = "sku"
                    }
                        );
                    Console.WriteLine(Convert.ToString(item.Quantity));
                }

                var payer = new Payer() { payment_method = "paypal" };

                var redirectURLs = new RedirectUrls()
                {
                    cancel_url = redirectURL + "&Cancel=true",
                    return_url = redirectURL
                };

                var details = new Details()
                {
                    tax = "0.00",
                    shipping = "0.00",
                    subtotal = Convert.ToString(Convert.ToDecimal(Session["SessionTotal"]))
                    // subtotal = "1"
                };

                var amount = new Amount()
                {
                    currency = "USD",
                    total = Convert.ToString(Convert.ToDecimal(Session["SessionTotal"])),
                    // total = "1",
                    details = details
                };

                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = Convert.ToString((new Random()).Next(100000)),
                    amount = amount,
                    item_list = itemList
                });

                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    redirect_urls = redirectURLs,
                    transactions = transactionList
                };
            }

            return this.payment.Create(apiContext);
        }
    }
}