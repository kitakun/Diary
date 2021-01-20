import React from 'react';
import './App.scss';
// Components
import Header from '../library/common/Layout/Header/Header';
import Footer from '../library/common/Layout/Footer/Footer';
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
