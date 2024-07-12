const CartSummary = ({ products, handleOrderNow }) => {
  const total = products.reduce((sum, product) => sum + product.price * product.quantity, 0);

  return (
    <div className="cart-summary">
      <h3>Total</h3>
      <div className="total-amount">${total.toFixed(2)}</div>
      <button className="order-now" onClick={handleOrderNow}>Order Now</button>
    </div>
  );
};

export default CartSummary;
