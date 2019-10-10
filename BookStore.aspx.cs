using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookStore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //get all books in the catalog.
            List<Book> books = BookCatalogDataAccess.GetAllBooks();
            foreach (Book book in books)
            {
                //todo: Populate dropdown list selections 
                ShoppingCart shoppingCart = (ShoppingCart) Session["shoppingcart"];
                bool has = false;

                if (shoppingCart != null && shoppingCart.BookOrders != null)
                {
                    foreach (BookOrder bookOrder in shoppingCart.BookOrders)
                    {
                        if (book.Id.Equals(bookOrder.Book.Id))
                        {
                            has = true;
                            break;
                        }
                    }
                }

                if (!has) {
                    drpBookSelection.Items.Add(new ListItem(book.Title, book.Id + ""));
                }
            }
        }
        ShoppingCart shoppingcart = null;
        if (Session["shoppingcart"] == null)
        {
            shoppingcart = new ShoppingCart();
            //todo: add cart to the session
            Session["shoppingcart"] = shoppingcart;
        }
        else
        {
            //todo: retrieve cart from the session
            shoppingcart = (ShoppingCart) Session["shoppingcart"];

            foreach (BookOrder order in shoppingcart.BookOrders)
            {
                //todo: Remove the book in the order from the dropdown list
                //done
            }
        }

        if (shoppingcart.NumOfItems == 0)
            lblNumItems.Text = "empty";
        else if (shoppingcart.NumOfItems == 1)
            lblNumItems.Text = "1 item";
        else
            lblNumItems.Text = shoppingcart.NumOfItems.ToString() + " items";
        
    }
    protected void drpBookSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpBookSelection.SelectedValue != "-1")
        {
            string bookId = drpBookSelection.SelectedItem.Value;
            Book selectedBook = BookCatalogDataAccess.GetBookById(bookId);

            //todo: Add selected book to the session
            Session["currentBook"] = selectedBook;

            //todo: Display the selected book's description and price 
            lblDescription.Text = selectedBook.Description;
            lblPrice.Text = "$ " + selectedBook.Price;
        }
        else
        {
            //todo: Set description and price to blank
            lblDescription.Text = "";
            lblPrice.Text = "";
        }
    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (quantityValidator.IsValid)
        {
            quantityValidator.CssClass = "hide";
        }

        if (Page.IsValid && drpBookSelection.SelectedValue != "-1" && Session["shoppingcart"] != null)
        {
            //todo: Retrieve selected book from the session
            Book selectedBook = (Book) Session["currentBook"];

            //todo: get user entered quqntity
            string str = txtQuantity.Text.ToString().Trim();

            int qnt = 0;
            try
            {
                qnt = Convert.ToInt32(str);
            }
            catch(Exception exc)
            {
                return;
            }

            if(qnt > 0)
            {
                //todo: Create a book order with selected book and quantity
                BookOrder order = new BookOrder(selectedBook, qnt);

                //todo: Retrieve to cart from the session
                ShoppingCart shoppingCart = (ShoppingCart)Session["shoppingcart"];

                //todo: Add book order to the shopping cart
                shoppingCart.AddBookOrder(order);
                Session["shoppingcart"] = shoppingCart;

                //todo: Remove the selected item from the dropdown list
                Response.Redirect("BookStore.aspx");
            }

            //todo: Set the dropdown list's selected value as "-1"

            //todo: Set the description to show title and quantity of the book user added to the shopping cart

            //todo: Update the number of items in shopping cart displayed next to the link to ShoppingCartView.aspx

        }
    }
}