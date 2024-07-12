import logo from './logo.svg';
import './App.css';
import {useEffect, useState} from "react";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import ProductList from './ProductList';
import CartSummary from './CartSummary';

import Checkout from './Checkout';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/checkout" element={<Checkout />} />
      </Routes>
    </Router>
  );
};

const Home = () => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    getProducts();
  }, []);

  async function getProducts() {
    try {
      const res = await fetch("http://localhost:5001/api/products");
      const data = await res.json();
      const productsWithQuantity = data.map(product => ({ ...product, quantity: 1 }));
      setProducts(productsWithQuantity);
    } catch (error) {
      console.error("Error fetching products:", error);
    }
  }

  const updateQuantity = (id, quantity) => {
    setProducts(products.map(product => 
      product.id === id ? { ...product, quantity: quantity } : product
    ));
  };

  const deleteProduct = id => {
    setProducts(products.filter(product => product.id !== id));
  };

  const handleOrderNow = () => {
    const orderItems = products.filter(product => product.quantity > 0);
    localStorage.setItem('orderItems', JSON.stringify(orderItems));
    window.location.href = '/checkout';
  };

    return (
    <div className="home">
      <div className="image-container">
        <img src="stand.png" alt="Lemonade Stand" className="centered-image" />
      </div>
      <div className="content">
        <ProductList products={products} updateQuantity={updateQuantity} deleteProduct={deleteProduct} />
        <CartSummary products={products} handleOrderNow={handleOrderNow} />
      </div>
    </div>
  );
};

export default App;
