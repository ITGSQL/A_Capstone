<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="WebForm1.aspx.vb" Inherits="LSS.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .itemA {
            height: 50px;
            width: 250px;
            margin: 20px;
            float: left;
            position: absolute;
        }

        .itemB {
            height: 150px;
            width: 250px;
            margin: 20px;
            float: left;
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="container" style="display: table; position: relative;">
        <div class="product medium">
			<div class="media">
				<a href="product.html" title="product title">
					<img src="img/product-2.jpg" alt="product title" data-img="product-2" class="img-responsive" />
				</a>
			</div>
			<div class="details">
				<p class="name"><a href="product.html">BeachFront Frog</a></p>
				<p class="price"><span class="cur">$</span><span class="total">28.00</span></p>
				<a href="" class="details-expand" data-target="details-0013">+</a>
			</div>
			<div class="details-extra" id="details-0013">
				<form class="form-inline" action="#">
					<div>
						<label>Quantity</label>	
						<input type="text" class="input-sm form-control quantity" value="1">
					</div>
					<div>
						<label>Size</label>
						<select class="input-sm form-control size">
							<option>S</option>
							<option>M</option>
							<option>L</option>										
						</select>
					</div>
				</form>
				<button class="btn btn-bottom btn-atc qadd">Add to cart</button>			
			</div>			
		</div>
    </div>
</asp:Content>
