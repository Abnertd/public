using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// 生成统计表
/// </summary>
public class Charts
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    Statistic statistic;
    
    public Charts()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        statistic = new Statistic();
    }


    //输出图像
    private void Output(Chart OutputChart)
    {
        MemoryStream MS = new MemoryStream();
        OutputChart.SaveImage(MS, ChartImageFormat.Png);
        MS.Seek(0, System.IO.SeekOrigin.Begin);
        Response.ContentType = "application/octet-stream";
        Response.BinaryWrite(MS.ToArray());
    }


    //销售额折线图表
    public void SaleChart(int Width, int Height)
    {
        Chart chart = new Chart();
        chart.Width = Width;
        chart.Height = Height;
        chart.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
        chart.BackSecondaryColor = System.Drawing.Color.FromArgb(212, 224, 240);
        chart.BackGradientStyle = GradientStyle.TopBottom;

        ChartArea ChartArea = chart.ChartAreas.Add("Default");
        ChartArea.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
        ChartArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);
        ChartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        ChartArea.AxisX.Interval = 2;
        ChartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        ChartArea.AxisY.LabelStyle.Font = new Font("Verdana", 8);

        Legend Legend = chart.Legends.Add("Default");
        Legend.Docking = Docking.Top;
        Legend.Alignment = StringAlignment.Near;
        Legend.BorderColor = Color.Black;
        Legend.LegendStyle = LegendStyle.Column;
        Legend.IsDockedInsideChartArea = true;
        Legend.DockedToChartArea = "Default";

        Series SaleSeries = chart.Series.Add("销售额");
        SaleSeries.ChartArea = "Default";
        SaleSeries.ChartType = SeriesChartType.Line;
        SaleSeries.IsValueShownAsLabel = true;
        SaleSeries["DrawingStyle"] = "LightToDark";
        SaleSeries.BorderWidth = 3;
        SaleSeries.Color = System.Drawing.Color.FromArgb(255, 153, 0);

        //填充数据
        //SaleSeries.Points.AddXY("2010-12-01", 100);
        //SaleSeries.Points.AddXY("2010-12-02", 90);
        //SaleSeries.Points.AddXY("2010-12-03", 120);
        //SaleSeries.Points.AddXY("2010-12-04", 100);
        //SaleSeries.Points.AddXY("2010-12-05", 120);
        //SaleSeries.Points.AddXY("2010-12-06", 10);
        //SaleSeries.Points.AddXY("2010-12-07", 60);
        //SaleSeries.Points.AddXY("2010-12-08", 170);
        //SaleSeries.Points.AddXY("2010-12-09", 180);
        //SaleSeries.Points.AddXY("2010-12-10", 190);


        int weekDay = (int)DateTime.Today.DayOfWeek;
        DateTime startDate = DateTime.Today.AddDays(-30);

        //if (weekDay == 0) { startDate = startDate.AddDays(-7); }
        //else { startDate = startDate.AddDays(-weekDay); }

        DateTime currentDate = DateTime.Today.AddDays(-30);
        double currentAllPrice = 0;

        for (int ii = 1; ii < 31; ii++)
        {
            currentDate = startDate.AddDays(ii);
            currentAllPrice = statistic.GetOrdersAllPriceByDate(currentDate.ToShortDateString(), currentDate.ToShortDateString(), "success");

            SaleSeries.Points.AddXY(currentDate.ToString("MM-dd"), currentAllPrice);
        }

        //for (int i = 0; i <= AxisX_Sale.Count - 1; i++)
        //{
        //    SaleSeries.Points.AddXY(AxisX_Sale(i), AxisY_Sale(i));
        //}

        Output(chart);
    }


    /// <summary>
    /// 统计报表中的销售统计
    /// </summary>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    public void SaleChart(int Width, int Height, DateTime startDate, DateTime endDate, string orders_status, string dataY)
    {

        TimeSpan DateInterval = endDate - startDate;
        int DayInterval = DateInterval.Days;

        Chart chart = new Chart();
        chart.Width = Width;
        chart.Height = Height;
        chart.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
        chart.BackSecondaryColor = System.Drawing.Color.FromArgb(212, 224, 240);
        chart.BackGradientStyle = GradientStyle.TopBottom;

        ChartArea ChartArea = chart.ChartAreas.Add("Default");
        ChartArea.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
        ChartArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);
        ChartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        ChartArea.AxisY.LabelStyle.Font = new Font("Verdana", 8);
        ChartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        

        Legend Legend = chart.Legends.Add("Default");
        Legend.Docking = Docking.Top;
        Legend.Alignment = StringAlignment.Near;
        Legend.BorderColor = Color.Black;
        Legend.LegendStyle = LegendStyle.Column;
        Legend.IsDockedInsideChartArea = true;
        Legend.DockedToChartArea = "Default";

        Series SaleSeries = new Series();
        if (dataY == "orders") {
            SaleSeries = chart.Series.Add("订单量");
        }
        else {
            SaleSeries = chart.Series.Add("销售额");
        }

        SaleSeries.ChartArea = "Default";
        SaleSeries.ChartType = SeriesChartType.Line;
        SaleSeries.IsValueShownAsLabel = true;
        SaleSeries["DrawingStyle"] = "LightToDark";
        SaleSeries.BorderWidth = 3;
        SaleSeries.Color = System.Drawing.Color.FromArgb(255, 153, 0);

        //填充数据
        DateTime currentDate = startDate;
        double currentAllPrice = 0;

        for (int ii = 0; ii < DayInterval+1; ii++)
        {
            currentDate = startDate.AddDays(ii);

            if (dataY == "orders")
            {
                currentAllPrice = statistic.GetOrdersCountByDate(currentDate.ToShortDateString(), currentDate.ToShortDateString(), orders_status);
            }
            else
            {
                 currentAllPrice = statistic.GetOrdersAllPriceByDate(currentDate.ToShortDateString(), currentDate.ToShortDateString(), orders_status);
            }

            SaleSeries.Points.AddXY(currentDate.ToString("MM-dd"), currentAllPrice);
        }
        Output(chart);
    }

}
