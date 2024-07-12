
import React, { useState } from 'react';
import './App.css';

const Checkout = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhone] = useState('');
  const [loading, setLoading] = useState(false);
  const orderItems = JSON.parse(localStorage.getItem('orderItems')) || [];

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      // 1. Create customer
      const customerRes = await fetch('http://localhost:5001/api/customers', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, email, phoneNumber }),
      });

      if (!customerRes.ok) {
        throw new Error('Failed to create customer');
      }

      const customerData = await customerRes.json();
      const customerId = customerData.id;

       // 2. Create order
      const orderDate = new Date().toISOString();
      const createdAt = orderDate;
      const updatedAt = orderDate;

      const orderRes = await fetch('http://localhost:5001/api/orders', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ customerId, orderDate, createdAt, updatedAt }),
      });

      if (!orderRes.ok) {
        throw new Error('Failed to create order');
      }

      const orderData = await orderRes.json();
      const orderId = orderData.id;

      // 3. Create order items
      for (const item of orderItems) {
        const orderItemRes = await fetch('http://localhost:5001/api/orderitems', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            orderId,
            productId: item.id,
            quantity: item.quantity,
            price: item.price,
          }),
        });

        if (!orderItemRes.ok) {
          throw new Error('Failed to create order item');
        }
      }

      localStorage.removeItem('orderItems');
      alert(`Order successful! Your order ID is ${orderId}`);

      setName('');
      setEmail('');
      setPhone('');

    } catch (error) {
      console.error('There was a problem with your fetch operation:', error);
      alert('Failed to place the order.');
    } finally {
      setLoading(false);
    }
  };

  return (
     <div className="checkout-page">
      <div className="image-container">
        <img src="stand.png" alt="Lemonade Stand" className="centered-image" />
      </div>
      <div className="checkout-form-container">
        <div className="cart-summary">
        <h2>Enter Your Details</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Name:</label>
            <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>Email:</label>
            <input type="text" value={email} onChange={(e) => setEmail(e.target.value)} required />
          </div>
          <div className="form-group">
            <label>Phone:</label>
            <input type="text" value={phoneNumber} onChange={(e) => setPhone(e.target.value)} required />
          </div>
          <button className="order-now" type="submit" disabled={loading}>{loading ? 'Processing...' : 'Submit Order'}</button>
        </form>
      </div>
      </div>
    </div>
  );
};

export default Checkout;


