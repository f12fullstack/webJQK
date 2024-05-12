﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/QuanTri.Master" AutoEventWireup="true" CodeBehind="QuanLyMyPham.aspx.cs" Inherits="TT_ShopJQK.Admin.QuanLyMyPham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- partial -->
          <div class="content-wrapper">
            <div class="page-header">
              <h3 class="page-title"> Bảng Sản Phẩm </h3>
              <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                  <li class="breadcrumb-item"><a href="#">Quản Lý Mỹ Phẩm</a></li>
                  <li class="breadcrumb-item active" >Bảng Sản Phẩm</li>
                </ol>
              </nav>
            </div>
            <div class="row">
                <div class="col-lg-12 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                   <div style="display:flex; justify-content:space-between;width:100%;padding-bottom:16px">
                       <div>
                        <h4 class="card-title">Sản Phẩm</h4>
                       </div>
                        <div>
                            <asp:Button ID="btThem" CssClass="btn btn-gradient-primary btn-fw" PostBackUrl="~/Admin/ThemSanPham.aspx"  runat="server" Text="Thêm Sản Phẩm"/><br />
                        </div>
                       
                   </div>

                      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
                          <Columns>
                              <asp:BoundField DataField="ProductId" HeaderText="Mã SP" />
                              <asp:BoundField DataField="CategoryId" HeaderText="Mã DM"  />
                              <asp:BoundField DataField="Name" HeaderText="Tên SP" />
                              <asp:BoundField DataField="LastPrice" HeaderText="Đơn Giá"  />
                              <asp:BoundField DataField="Price" HeaderText="Giá Ảo"  />
                              <asp:BoundField DataField="Description" HeaderStyle-Width="350px" HeaderText="Thông Tin SP"  />
                              <%--<asp:BoundField DataField="Url" HeaderText="hinhAnh" />--%>
            <asp:TemplateField HeaderText="Hình Ảnh" >
                <EditItemTemplate>
                    <asp:TextBox ID="anhhh" runat="server" Text='<%#Eval ("Url").ToString() %>'></asp:TextBox>
                </EditItemTemplate>
                 <ItemTemplate>
                     <asp:Image Width="130px" Height="130px" ID="Image1" runat="server"  ImageUrl='<%#: ((List<string>)Eval("Url"))[0] %>' />
                 </ItemTemplate>              
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Xoá">
                <ItemTemplate>
                    <asp:Button ID="buXoa" CommandName="xoa" CommandArgument='<%# Bind("ProductId") %>' Text="Xoá" 
                        OnCommand="Xoa_click" CssClass="btn btn-gradient-primary btn-sm" runat="server" OnClientClick="return confirm('xoa di!')" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sửa">
                <ItemTemplate>
                    <asp:Button ID="btnSua" CommandName="sua"  CssClass="btn btn-gradient-primary btn-sm"
                        CommandArgument='<%# Bind("ProductId") %>' Text="Sửa" 
                        OnCommand="Sua_click" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

                          </Columns>
                               <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
                      </asp:GridView>
                      
                  </div>
                </div>
              </div>
             
            </div>
          </div>
          
    <script src="../../assets/vendors/js/vendor.bundle.base.js"></script>
    <script src="../../assets/js/off-canvas.js"></script>
    <script src="../../assets/js/hoverable-collapse.js"></script>
    <script src="../../assets/js/misc.js"></script>
</asp:Content>