<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vnpay_refund.aspx.cs" Inherits="TT_ShopJQK.vnpay_refund" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RETURN URL FROM VNPAY</title>
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/iconfont.min.css" />
    <link rel="stylesheet" href="assets/css/plugins.css" />
    <link rel="stylesheet" href="assets/css/helper.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
    <!-- Modernizr JS -->
    <script src="assets/js/vendor/modernizr-2.8.3.min.js"></script>
</head>
<body>
    <div class="container">
        <h3 class="text-muted" >Kết quả thanh toán</h3>
           <div runat="server" id="displayMsg"></div>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th scope="col"></th>
                    <th scope="col">Name</th>
                    <th scope="col">Value</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th scope="row">1</th>
                    <td>
                        <span runat="server" id="displayTmnCode"></span>
                    </td>
                    <td >
                        <span runat="server" id="value_displayTmnCode"></span>
                    </td>
                </tr>
                <tr>
                    <th scope="row">2</th>
                    <td>
                        <div runat="server" id="displayTxnRef"></div>
                    </td>
                    <td  runat="server" id="value_displayTxnRef" >

                    </td>
                </tr>
                <tr>
                    <th scope="row">3</th>
                    <td>
                        <div runat="server" id="displayVnpayTranNo"></div>
                    </td>
                    <td>
                        <div runat="server" id="value_displayVnpayTranNo"></div>
                    </td>
                </tr>
                <tr>
                    <th scope="row">4</th>
                    <td>
                        <div runat="server" id="displayAmount"></div>
                    </td>
                    <td>
                        <div runat="server" id="value_displayAmount"></div>
                    </td>
                </tr>
                <tr>
                    <th scope="row">5</th>
                    <td>
                          <div runat="server" id="displayBankCode"></div>
                    </td>
                    <td>
                        <div runat="server" id="value_displayBankCode"></div>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <a href="TrangChu.aspx">Quay lại trang chủ</a>
    </div>
</body>
</html>
