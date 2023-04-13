using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace MockFood.Pages
{
    public class PaymentModel : PageModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void pay_now_Click(object sender, EventArgs e)
        {
            string result = "";
            string url = "https://api.payserv.net/v1/rpf/transactions/rpf";
            string mid = "0000001404114A546C5D";
            string mkey = "13ADA55C6A72E2FF0284DF82B80CFA74";
            string authUser = "pnx_test";
            string authPass = "p45jr5taqkc22al";

            try
            {
                string authData = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authUser + ":" + authPass));

                List<orders> list_orders = new List<orders>();
                string itemname = order_itemname_value.Text;
                string qty = order_itemqty_value.Text;
                double itemamount = Convert.ToDouble(order_itemamount_value.Text);
                double subtotal = Convert.ToDouble(qty) * itemamount;
                double shippingamount = Convert.ToDouble(order_shipping_value.Text);
                double discountamount = 0;
                double total = (subtotal + shippingamount) - discountamount;
                list_orders.Add(new orders { itemname = itemname, quantity = qty.ToString(), unitprice = itemamount.ToString("F2"), totalprice = subtotal.ToString("F2") });

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                httpWebRequest.ContentType = "application/json";
                string authInfo = authUser + ":" + authPass;
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                httpWebRequest.Headers["Authorization"] = "Basic " + authInfo;
                httpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    workRequest request = new workflowRequest();

                    orders[] _orders = list_orders.Cast<orders>().ToArray();
                    order_details order_details = new order_details();
                    order_details.orders = _orders;
                    order_details.subtotalprice = subtotal.ToString("F2");
                    order_details.shippingprice = shippingamount.ToString("F2");
                    order_details.discountamount = discountamount.ToString("F2");
                    order_details.totalorderamount = total.ToString("F2");

                    request.order_details = order_details;

                    transaction trx = new transaction();
                    trx.request_id = Utility.generateGUID();
                    trx.notification_url = "https://testpti.payserv.net/parker-projects/workflow-demo/callback.aspx";
                    trx.response_url = "https://testpti.payserv.net/parker-projects/workflow-demo/response.aspx";
                    trx.cancel_url = "https://testpti.payserv.net/parker-projects/workflow-demo/response.aspx";
                    trx.pmethod = "onlinebanktransfer";
                    trx.pchannel = "br_bdo_ph";
                    trx.payment_action = "url_link";
                    trx.collection_method = "single_pay";
                    trx.payment_notification_status = "1";
                    trx.payment_notification_channel = "1";
                    trx.amount = total.ToString("F2");
                    trx.currency = "PHP";
                    string trxSign = mid + trx.request_id + trx.notification_url + trx.response_url + trx.cancel_url + trx.pmethod
                        + trx.payment_action + trx.collection_method + trx.amount + trx.currency
                        + trx.payment_notification_status + trx.payment_notification_channel;
                    trx.signature = Utility.SignCertificate(trxSign, mkey);

                    request.transaction = trx;

                    customer_info cinfo = new customer_info();
                    cinfo.fname = fname_value.Text;
                    cinfo.lname = lname_value.Text;
                    cinfo.mname = "";
                    cinfo.email = email_value.Text;
                    cinfo.phone = "";
                    cinfo.mobile = mobile_value.Text;
                    cinfo.dob = "";
                    string custSign = cinfo.fname + cinfo.lname + cinfo.mname + cinfo.email + cinfo.phone + cinfo.mobile + cinfo.dob;
                    cinfo.signature = Utility.SignCertificate(custSign, mkey);
                    request.customer_info = cinfo;

                    billing_info billing = new billing_info();
                    billing.billing_address1 = bill_addr1_value.Text;
                    billing.billing_address2 = bill_addr2_value.Text;
                    billing.billing_city = bill_city_value.Text;
                    billing.billing_state = bill_state_value.Text;
                    billing.billing_country = bill_country_value.SelectedValue;
                    billing.billing_zip = bill_zip_value.Text;
                    request.billing_info = billing;

                    shipping_info shipping = new shipping_info();
                    shipping.shipping_address1 = ship_addr1_value.Text;
                    shipping.shipping_address2 = ship_addr2_value.Text;
                    shipping.shipping_city = ship_city_value.Text;
                    shipping.shipping_state = ship_state_value.Text;
                    shipping.shipping_country = ship_country_value.SelectedValue;
                    shipping.shipping_zip = ship_zip_value.Text;
                    request.shipping_info = shipping;

                    var json = new JavaScriptSerializer().Serialize(request);
                    string jsondata = json;
                    log.log("JSON Request: " + jsondata);
                    streamWriter.Write(jsondata);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                    log.log("Initial Response: " + result);

                    workflowResponse responseObject = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<workflowResponse>(result);
                    if (responseObject.payment_action_info != "" && responseObject.payment_action_info != null)
                    {
                        log.log("Redirect to: " + responseObject.payment_action_info);
                        Response.Redirect(responseObject.payment_action_info, false);
                    }
                }
            }
            catch (WebException wex)
            {
                log.log("Exception: " + wex.Message + " : " + wex.StackTrace + " : " + wex.InnerException);
                result = new StreamReader(wex.Response.GetResponseStream()).ReadToEnd();
                var webResponse = wex.Response as System.Net.HttpWebResponse;
                using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    workflowResponse responseObject = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<workflowResponse>(result);
                    if (responseObject.payment_action_info != "" && responseObject.payment_action_info != null)
                    {
                        Response.Redirect(responseObject.payment_action_info, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("https://testpti.payserv.net/parker-projects/workflow-demo/response.aspx");
                log.log("Exception: " + ex.Message + " : " + ex.StackTrace + " : " + ex.InnerException);
            }
        }
    }
