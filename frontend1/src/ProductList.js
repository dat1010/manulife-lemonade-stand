import React from 'react';
import ProductItem from './ProductItem';

const ProductList = ({ products, updateQuantity, deleteProduct }) => {
  return (
    <div className="product-list">
      {products.map(product => (
        <ProductItem 
          key={product.id} 
          product={product} 
          updateQuantity={updateQuantity} 
          deleteProduct={deleteProduct} 
        />
      ))}
    </div>
  );
};

export default ProductList;
