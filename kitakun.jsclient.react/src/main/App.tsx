import React from 'react';
import './App.css';
// Components
import Header from '../library/common/Header/Header';
import Footer from '../library/common/Footer/Footer';
import AppRoutes from './routes/AppRoutes';

function App() {
  return (
    <div className="App">
      <Header></Header>
      <AppRoutes></AppRoutes>
      <Footer></Footer>
    </div>
  );
}

export default App;
