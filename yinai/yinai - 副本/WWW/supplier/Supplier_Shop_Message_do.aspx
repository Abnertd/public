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


            //ɾ��ѡ�е���Ϣ
            case "suppliermessagesmove":
                MyApp.SupplierMessges_Del(false);
                break;
            //ɾ���û�Ա���ܵ����е���Ϣ
            case "supplierallmessagesmove":
                MyApp.SupplierMessges_Del(true);
                break;
            //���Ϊ�Ѷ�
            case "suppliermessagesIsRead":
                MyApp.SupplierMessges_IsRead(1);
                break;
            //���Ϊδ��
            case "suppliermessagesIsUnRead":
                MyApp.SupplierMessges_IsRead(0);
                break;
            //ȫ�����Ϊ�Ѷ�
            case "supplierallmessagesIsRead":
                MyApp.AllSupplierMessges_IsRead(1);
                break;
            
        }

    }
</script>
