using System;

public class Paging
{

    public string DisplayPage(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    {
        string Phtml = "";

        Phtml = "<table border=\"0\" cellspacing=\"1\" cellpadding=\"0\" height=\"26\">";
        Phtml += "  <tr align=\"center\" valign=\"middle\">";
        if (currentpage <= 1)
        {
            Phtml += "<td class=\"page_off\">&#171;上一页</td>";
        }
        else
        {
            Phtml += "<td class=\"page_on\"><a href=\"" + pageurl.Replace("{page}", (currentpage - 1).ToString()) + "\" class=\"page_on_t\">&#171;上一页</a></td>";
        }
        if (pagecount <= 12)
        {
            for (int ipage = 1; ipage <= pagecount; ipage++)
            {
                if (currentpage == ipage)
                {
                    Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                }
                else
                {
                    Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                }
            }
        }
        else if (pagecount > 12 && pagecount < 16)
        {
            if (currentpage < 9)
            {
                for (int ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
                Phtml += "<td class=\"page_omit\">&#8230;</td>";
                for (int ipage = (pagecount - 1); ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
            }
            else
            {
                for (short ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
                Phtml += "<td class=\"page_omit\">&#8230;</td>";
                for (int ipage = (pagecount - 9); ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
            }
        }
        else if (pagecount >= 16)
        {
            if (currentpage < 9)
            {
                for (int ipage = 1; ipage <= 10; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
                Phtml += "<td class=\"page_omit\">&#8230;</td>";
                for (int ipage = (pagecount - 1); ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
            }
            else if (currentpage + 7 > pagecount)
            {
                for (int ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
                Phtml += "<td class=\"page_omit\">&#8230;</td>";
                for (int ipage = (pagecount - 9); ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
            }
            else
            {
                for (int ipage = 1; ipage <= 2; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
                Phtml += "<td class=\"page_omit\">&#8230;</td>";
                for (int ipage = (currentpage - 5); ipage <= (currentpage + 4); ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
                Phtml += "<td class=\"page_omit\">&#8230;</td>";
                for (int ipage = (pagecount - 1); ipage <= pagecount; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        Phtml += "<td class=\"page_current\">" + ipage + "</td>";
                    }
                    else
                    {
                        Phtml += "<td class=\"page_num\"><a href=\"" + pageurl.Replace("{page}", ipage.ToString()) + "\" class=\"page_num_t\">" + ipage + "</a></td>";
                    }
                }
            }
        }
        if (currentpage == pagecount)
        {
            Phtml += "<td class=\"page_off\">下一页&#187;</td>";
        }
        else
        {
            Phtml += "<td class=\"page_on\"><a href=\"" + pageurl.Replace("{page}", (currentpage + 1).ToString()) + "\" class=\"page_on_t\">下一页&#187;</a></td>";
        }
        Phtml += "  </tr></table>";
        return Phtml;
    }
}