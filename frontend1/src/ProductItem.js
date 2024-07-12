import React from 'react';

const ProductItem = ({ product, updateQuantity, deleteProduct }) => {
  const handleQuantityChange = (e) => {
    updateQuantity(product.id, parseInt(e.target.value));
  };

  return (
    <div className="product-item">
      <img src="lemon.png" alt="Lemon" style={{ width: '50px', height: '30px' }} />
      <div className="product-details">
        <h4>{product.type}</h4>
        <p>{product.size}</p>
      </div>
      <div className="product-price">{product.price.toFixed(2)}</div>
      <div className="product-quantity">
        <button onClick={() => updateQuantity(product.id, product.quantity - 1)} disabled={product.quantity <= 1}>-</button>
        <input type="number" value={product.quantity} onChange={handleQuantityChange} />
        <button onClick={() => updateQuantity(product.id, product.quantity + 1)}>+</button>
      </div>
      <div className="product-total">{(product.price * product.quantity).toFixed(2)}</div>
      <button onClick={() => deleteProduct(product.id)}>🗑️</button>
    </div>
  );
};

export default ProductItem;
