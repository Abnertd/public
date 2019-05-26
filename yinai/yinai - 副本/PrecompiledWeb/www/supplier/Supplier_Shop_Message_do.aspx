<%@ Page Language="C#" %>

<script runat="server">
    Supplier supplier;
    protected void Page_Load(object sender, EventArgs e)
    {
        supplier = new Supplier();
        supplier.Supplier_Login_Check("/supplier/Supplier_Shop_Message.aspx");
        Supplier MyApp = new Supplier();
        string action = Request["action"];
        switch (action)
        {
            case "del":
                MyApp.SupplierShopAsk_Del();
                break;
            case "reply":
                MyApp.SupplierShopAsk_Reply();
                break;


            //删除选中的消息
            case "suppliermessagesmove":
                MyApp.SupplierMessges_Del(false);
                break;
            //删除该会员接受的所有的消息
            case "supplierallmessagesmove":
                MyApp.SupplierMessges_Del(true);
                break;
            //标记为已读
            case "suppliermessagesIsRead":
                MyApp.SupplierMessges_IsRead(1);
                break;
            //标记为未读
            case "suppliermessagesIsUnRead":
                MyApp.SupplierMessges_IsRead(0);
                break;
            //全部标记为已读
            case "supplierallmessagesIsRead":
                MyApp.AllSupplierMessges_IsRead(1);
                break;
            
        }

    }
</script>
