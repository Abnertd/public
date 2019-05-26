<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Logistics myApp = new Logistics();
        Contract contract = new Contract();

        Glaer.Trade.Util.Tools.ITools tools;
        tools = Glaer.Trade.Util.Tools.ToolsFactory.CreateTools();
        string action = Request["action"];
        string Contract_ID = Request["Contract_ID"];
      
        int Contract_Id = Convert.ToInt32(Contract_ID);
      string orders_sn=  Request["orders_sn"];   
        switch (action)
        {

            case "contract_save":
                contract.edit_contract(Contract_Id,orders_sn);
                break;               
        }
    }
</script>