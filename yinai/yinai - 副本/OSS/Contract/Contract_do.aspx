<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<script runat="server">
    private Contract myApp;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Contract();
        tools = ToolsFactory.CreateTools();
        string action = Request["action"];
        switch (action)
        {
            case "addtmpcontract":
                Public.CheckLogin("010afb3b-1cbf-47f9-8455-c35fe5eceea7");
                myApp.Create_TmpContract("");
                break;
            case "list":
                Public.CheckLogin("a3465003-08b3-4a31-9103-28d16c57f2c8");

                Response.Write(myApp.GetContracts());
                Response.End();
                break;
            case "Contract_Save":
                Public.CheckLogin("cd2be0f8-b35a-48ad-908b-b5165c0a1581");
                myApp.Contract_Edit();
                break;
            case "order_contract":
                Public.CheckLogin("all");
                myApp.Contract_Order_Add();
                break;
            case "order_remove":
                Public.CheckLogin("all");
                myApp.TmpContract_Orders_Remove();
                break;
            case "Contract_dividedSave":
                Public.CheckLogin("all");
                myApp.Contract_Divided_Set();
                break;
            case "contract_noteedit":
                Public.CheckLogin("all");
                myApp.TmpContract_NoteEdit();
                break;
            case "contract_cancel":

                myApp.Contract_Close();
                break;
            case "confirm":
                Public.CheckLogin("b27825e2-74c7-498b-9c83-ce0f460e9bda");
                myApp.TmpContract_SysConfirm();
                break;
            case "prepare":
                Public.CheckLogin("5b7c6861-fae3-4a5b-a72d-505bdd56b73b");
                myApp.Contract_Delivery_Prepare();
                break;
            case "contract_freight":
                Public.CheckLogin("4b60b2fa-95b4-493a-a0b8-d1a06913b9b4");
                myApp.Contract_Dilivery_Action("create");
                break;
            case "contract_pay":
                Public.CheckLogin("f0a5a5f7-c145-4a58-9780-205b406d266e");
                myApp.Contract_Payment_Action("create");
                break;
            case "contract_paid":
                Public.CheckLogin("f0a5a5f7-c145-4a58-9780-205b406d266e");
                myApp.Contract_Payment_Already();
                break;
            case "contract_payreach":
                Public.CheckLogin("171ba01c-ecd2-4f1c-9ab2-1cc026b48480");
                myApp.Contract_Payment_Reach();
                break;
            case "contract_accept":
                Public.CheckLogin("31c6736d-9517-42ac-9e99-0c5590070072");
                myApp.Contract_Delivery_Accept();
                break;
            case "contract_allaccept":
                Public.CheckLogin("31c6736d-9517-42ac-9e99-0c5590070072");
                myApp.Contract_Delivery_AllAccept();
                break;
            case "contract_success":
                Public.CheckLogin("16770a1b-f216-453d-8210-6e22e6ac364e");
                myApp.Contract_Finish();
                break;


            case "invoicestatus":
                Public.CheckLogin("2233c05d-4d76-43c6-b680-74df5ece66b2");
                myApp.UpdateInvoiceStatus();
                break;
            case "apply_accept":
                Public.CheckLogin("31c6736d-9517-42ac-9e99-0c5590070072");
                myApp.UpdateInvoiceApplyStatus(3);
                break;
            case "apply_cancel":
                Public.CheckLogin("31c6736d-9517-42ac-9e99-0c5590070072");
                myApp.UpdateInvoiceApplyStatus(4);
                break;
            case "apply_open":
                Public.CheckLogin("31c6736d-9517-42ac-9e99-0c5590070072");
                myApp.UpdateInvoiceApplyStatus(1);
                break;
            case "apply_send":
                Public.CheckLogin("31c6736d-9517-42ac-9e99-0c5590070072");
                myApp.UpdateInvoiceApplyStatus(2);
                break;
            
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        myApp = null;
        tools = null;
    }
</script>
