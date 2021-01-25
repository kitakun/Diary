import React from 'react';
import ReactDOM from 'react-dom';
import { Router } from 'react-router-dom';
import { createBrowserHistory } from "history";
import reportWebVitals from './reportWebVitals';
// redux
import { applyMiddleware, combineReducers, createStore } from 'redux';
import { Provider } from 'react-redux';
import thunk from 'redux-thunk';
import { composeWithDevTools } from 'redux-devtools-extension';
// redux stores
import spaceReducer from './store/space/space.reducer';
// Locals
import App from './main/App';
import { IRootStore } from 'types';

const customHistory = createBrowserHistory();
// redux
const rootReducer = combineReducers<IRootStore>(
  {
    spaceStore: spaceReducer,
  });
const reduxDevStore = composeWithDevTools(applyMiddleware(thunk));
const rootStore = createStore(rootReducer, reduxDevStore);

ReactDOM.render(
  <React.StrictMode>
    <Provider store={rootStore}>
      <Router history={customHistory}>
        <App />
      </Router>
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

export { customHistory };