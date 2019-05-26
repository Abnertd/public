using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.Model
{
    #region 用户信贷信息查询

    public class QueryMemberLoanJsonInfo 
    {  
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private double _credit_limit;
        public double Credit_limit
        {
            get { return _credit_limit; }
            set { _credit_limit = value; }
        }

        private double _apply_credit;
        public double Apply_credit
        {
            get { return _apply_credit; }
            set { _apply_credit = value; }
        }

        private double _used_credit;
        public double Used_credit
        {
            get { return _used_credit; }
            set { _used_credit = value; }
        }

        private double _margin_balance;
        public double Margin_balance
        {
            get { return _margin_balance; }
            set { _margin_balance = value; }
        }

        private double _margin_freeze;
        public double Margin_freeze
        {
            get { return _margin_freeze; }
            set { _margin_freeze = value; }
        }

        private double _available_credit;
        public double Available_credit
        {
            get { return _available_credit; }
            set { _available_credit = value; }
        }
    }

    #endregion

    #region 信贷产品信息查询
    public class QueryLoanProductJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _agreement_no;
        public string Agreement_no
        {
            get { return _agreement_no; }
            set { _agreement_no = value; }
        }

        private IList<LoanProductTermInfo> _loan_term_list;
        public IList<LoanProductTermInfo> Loan_term_list
        {
            get { return _loan_term_list; }
            set { _loan_term_list = value; }
        }

        private IList<LoanProductMethodInfo> _repay_method_list;
        public IList<LoanProductMethodInfo> Repay_method_list
        {
            get { return _repay_method_list; }
            set { _repay_method_list = value; }
        }

        private IList<LoanProductFeeInfo> _fee_list;
        public IList<LoanProductFeeInfo> Fee_list
        {
            get { return _fee_list; }
            set { _fee_list = value; }
        }

        private string _fee_account;
        public string Fee_account
        {
            get { return _fee_account; }
            set { _fee_account = value; }
        }

        private string _fee_member;
        public string Fee_member
        {
            get { return _fee_member; }
            set { _fee_member = value; }
        }

        private string _margin_rate;
        public string Margin_rate
        {
            get { return _margin_rate; }
            set { _margin_rate = value; }
        }
    }
      
    public class LoanProductTermInfo
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _loan_term;
        public string Loan_term
        {
            get { return _loan_term; }
            set { _loan_term = value; }
        }

        private string _term_unit;
        public string Term_unit
        {
            get { return _term_unit; }
            set { _term_unit = value; }
        }

        private string _interest_rate;
        public string Interest_rate
        {
            get { return _interest_rate; }
            set { _interest_rate = value; }
        }

        private string _interest_rate_unit;
        public string Interest_rate_unit
        {
            get { return _interest_rate_unit; }
            set { _interest_rate_unit = value; }
        }
    }

    public class LoanProductMethodInfo
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _repay_method_type;
        public string Repay_method_type
        {
            get { return _repay_method_type; }
            set { _repay_method_type = value; }
        }
    }

    public class LoanProductFeeInfo
    {
        private string _fee_rate;
        public string Fee_rate
        {
            get { return _fee_rate; }
            set { _fee_rate = value; }
        }
    }

    #endregion

    #region 信贷申请

    public class LoanApplyJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _loan_proj_no;
        public string Loan_proj_no
        {
            get { return _loan_proj_no; }
            set { _loan_proj_no = value; }
        }

        private double _loan_amount;
        public double Loan_amount
        {
            get { return _loan_amount; }
            set { _loan_amount = value; }
        }

        private double _interest_rate;
        public double Interest_rate
        {
            get { return _interest_rate; }
            set { _interest_rate = value; }
        }

        private string _interest_rate_unit;
        public string Interest_rate_unit
        {
            get { return _interest_rate_unit; }
            set { _interest_rate_unit = value; }
        }

        private double _term;
        public double Term
        {
            get { return _term; }
            set { _term = value; }
        }

        private string _term_unit;
        public string Term_unit
        {
            get { return _term_unit; }
            set { _term_unit = value; }
        }

        private double _fee_amount;
        public double Fee_amount
        {
            get { return _fee_amount; }
            set { _fee_amount = value; }
        }

        private string _repay_method;
        public string Repay_method
        {
            get { return _repay_method; }
            set { _repay_method = value; }
        }

        private double _margin_amount;
        public double Margin_amount
        {
            get { return _margin_amount; }
            set { _margin_amount = value; }
        }
    }

    #endregion

    #region 贷款推进

    public class LoanPushJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _loan_proj_no;
        public string Loan_proj_no
        {
            get { return _loan_proj_no; }
            set { _loan_proj_no = value; }
        }

        private string _push_url;
        public string Push_url
        {
            get { return _push_url; }
            set { _push_url = value; }
        }
    }

    #endregion

    #region 信贷信息查询

    public class QueryLoanProjectJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private int _all_count;
        public int All_count
        {
            get { return _all_count; }
            set { _all_count = value; }
        }

        private int _current_page;
        public int Current_page
        {
            get { return _current_page; }
            set { _current_page = value; }
        }

        private int _page_size;
        public int Page_size
        {
            get { return _page_size; }
            set { _page_size = value; }
        }

        private IList<LoanlistInfo> _loan_list;
        public IList<LoanlistInfo> Loan_list
        {
            get { return _loan_list; }
            set { _loan_list = value; }
        }
    }

    public class LoanlistInfo
    {
        private double _advance_amount;
        public double Advance_amount
        {
            get { return _advance_amount; }
            set { _advance_amount = value; }
        }

        private string _current_repay_date;
        public string Current_repay_date
        {
            get { return _current_repay_date; }
            set { _current_repay_date = value; }
        }

        private double _fee_amount;
        public double Fee_amount
        {
            get { return _fee_amount; }
            set { _fee_amount = value; }
        }

        private double _interest_amount;
        public double Interest_amount
        {
            get { return _interest_amount; }
            set { _interest_amount = value; }
        }

        private double _interest_rate;
        public double Interest_rate
        {
            get { return _interest_rate; }
            set { _interest_rate = value; }
        }

        private string _interest_rate_unit;
        public string Interest_rate_unit
        {
            get { return _interest_rate_unit; }
            set { _interest_rate_unit = value; }
        }

        private string _issue_date;
        public string Issue_date
        {
            get { return _issue_date; }
            set { _issue_date = value; }
        }

        private string _loan_proj_no;
        public string Loan_proj_no
        {
            get { return _loan_proj_no; }
            set { _loan_proj_no = value; }
        }

        private string _loan_status;
        public string Loan_status
        {
            get { return _loan_status; }
            set { _loan_status = value; }
        }

        private double _margin_amount;
        public double Margin_amount
        {
            get { return _margin_amount; }
            set { _margin_amount = value; }
        }

        private string _outer_order_no;
        public string Outer_order_no
        {
            get { return _outer_order_no; }
            set { _outer_order_no = value; }
        }

        private double _principal_amount;
        public double Principal_amount
        {
            get { return _principal_amount; }
            set { _principal_amount = value; }
        }

        private double _repay_amount;
        public double Repay_amount
        {
            get { return _repay_amount; }
            set { _repay_amount = value; }
        }

        private string _repay_method;
        public string Repay_method
        {
            get { return _repay_method; }
            set { _repay_method = value; }
        }

        private string _submit_date;
        public string Submit_date
        {
            get { return _submit_date; }
            set { _submit_date = value; }
        }

        private string _term;
        public string Term
        {
            get { return _term; }
            set { _term = value; }
        }

        private string _term_unit;
        public string Term_unit
        {
            get { return _term_unit; }
            set { _term_unit = value; }
        }

        private double _unfreeze_amount;
        public double Unfreeze_amount
        {
            get { return _unfreeze_amount; }
            set { _unfreeze_amount = value; }
        }

        private double _unpaid_fee;
        public double Unpaid_fee
        {
            get { return _unpaid_fee; }
            set { _unpaid_fee = value; }
        }

        private double _unpaid_interest;
        public double Unpaid_interest
        {
            get { return _unpaid_interest; }
            set { _unpaid_interest = value; }
        }

        private double _unpaid_principal;
        public double Unpaid_principal
        {
            get { return _unpaid_principal; }
            set { _unpaid_principal = value; }
        }     
    }

    #endregion

    #region 信贷分期详情查询

    public class QueryProjectDetailJsonInfo 
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _loan_proj_no;
        public string Loan_proj_no
        {
            get { return _loan_proj_no; }
            set { _loan_proj_no = value; }
        }

        private string _begin_date;
        public string Begin_date
        {
            get { return _begin_date; }
            set { _begin_date = value; }
        }

        private string _end_date;
        public string End_date
        {
            get { return _end_date; }
            set { _end_date = value; }
        }

        private int _all_term;
        public int All_term
        {
            get { return _all_term; }
            set { _all_term = value; }
        }

        private IList<QueryProjectDetailTremInfo> _installment_list;
        public IList<QueryProjectDetailTremInfo> Installment_list 
        { 
            get { return _installment_list; }
            set { _installment_list = value; }
        }
    }

    public class QueryProjectDetailTremInfo
    {
        private int _term;
        public int Term
        {
            get { return _term; }
            set { _term = value; }
        }

        private double _principal;
        public double Principal
        {
            get { return _principal; }
            set { _principal = value; }
        }

        private double _interest;
        public double Interest
        {
            get { return _interest; }
            set { _interest = value; }
        }

        private double _fee;
        public double Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        private double _penalty;
        public double Penalty
        {
            get { return _penalty; }
            set { _penalty = value; }
        }

        private double _unpaid_principal;
        public double Unpaid_principal
        {
            get { return _unpaid_principal; }
            set { _unpaid_principal = value; }
        }

        private double _unpaid_interest;
        public double Unpaid_interest
        {
            get { return _unpaid_interest; }
            set { _unpaid_interest = value; }
        }

        private double _unpaid_fee;
        public double Unpaid_fee
        {
            get { return _unpaid_fee; }
            set { _unpaid_fee = value; }
        }

        private double _unpaid_penalty;
        public double Unpaid_penalty
        {
            get { return _unpaid_penalty; }
            set { _unpaid_penalty = value; }
        }

        private string _repay_date;
        public string Repay_date
        {
            get { return _repay_date; }
            set { _repay_date = value; }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }

    #endregion

    #region 贷款还款

    public class LoanRepaymentJsonInfo
    { 
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _loan_proj_no;
        public string Loan_proj_no
        {
            get { return _loan_proj_no; }
            set { _loan_proj_no = value; }
        }

        private string _repay_url;
        public string Repay_url
        {
            get { return _repay_url; }
            set { _repay_url = value; }
        }
    }

    #endregion

    #region 贷款撤销

    public class LoanCancelJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }

    #endregion

    #region 保证金转余额

    public class MarginToBalanceJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _success_amount;
        public string Success_amount
        {
            get { return _success_amount; }
            set { _success_amount = value; }
        }
    }

    #endregion

    #region 即时金价接口

    public class GoldJsonInfo
    {
        private int _error_code;
        public int Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _reason;
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        private IList<GoldResultInfo> _result;
        public IList<GoldResultInfo> Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }

    public class GoldResultInfo
    {
        private string _variety;
        public string Variety
        {
            get { return _variety; }
            set { _variety = value; }
        }

        private string _latestpri;
        public string Latestpri
        {
            get { return _latestpri; }
            set { _latestpri = value; }
        }

        private string _openpri;
        public string Openpri
        {
            get { return _openpri; }
            set { _openpri = value; }
        }

        private string _maxpri;
        public string Maxpri
        {
            get { return _maxpri; }
            set { _maxpri = value; }
        }

        private string _minpri;
        public string Minpri
        {
            get { return _minpri; }
            set { _minpri = value; }
        }

        private string _limit;
        public string Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        private string _yespri;
        public string Yespri
        {
            get { return _yespri; }
            set { _yespri = value; }
        }

        private string _totalvol;
        public string Totalvol
        {
            get { return _totalvol; }
            set { _totalvol = value; }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }

    #endregion

    #region 担保交易

    public class TradeJsonInfo
    {
        private string _is_success;
        public string Is_success
        {
            get { return _is_success; }
            set { _is_success = value; }
        }

        private string _partner_id;
        public string Partner_id
        {
            get { return _partner_id; }
            set { _partner_id = value; }
        }

        private string __input_charset;
        public string _input_charset
        {
            get { return __input_charset; }
            set { __input_charset = value; }
        }

        private string _sign;
        public string Sign
        {
            get { return _sign; }
            set { _sign = value; }
        }

        private string _sign_type;
        public string Sign_type
        {
            get { return _sign_type; }
            set { _sign_type = value; }
        }        

        private string _error_code;
        public string Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        private string _error_message;
        public string Error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }

        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private string _cashier_url;
        public string Cashier_url
        {
            get { return _cashier_url; }
            set { _cashier_url = value; }
        }

        private string _inst_order_no;
        public string Inst_order_no
        {
            get { return _inst_order_no; }
            set { _inst_order_no = value; }
        }
    }

    #endregion

}
