// import ProductDetails from "../../features/catalog/ProductDetails";
// import CheckoutWrapper from "../../features/checkout/CheckoutWrapper";
// import ContactPage from "../../features/contact/ContactPage";
// import Orders from "../../features/orders/Orders";
// import NotFound from "../errors/NotFound";
// import ServerError from "../errors/ServerError";
import App from "../layout/App";
import IncomesTransactions from "../../features/incomes/IncomesTransactions";
import Dashboard from "../../features/dashboard/Dashboard";
// import RequireAuth from "./RequireAuth";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            // // authenticated routes
            // {element: <RequireAuth />, children: [
            //     {path: 'checkout', element: <CheckoutWrapper />},
            //     {path: 'orders', element: <Orders />},
            // ]},
            // // admin routes
            // {element: <RequireAuth roles={['Admin']} />, children: [
            //     {path: 'inventory', element: <Inventory />},
            // ]},
            {path: 'dashboard', element: <Dashboard />},
            {path: 'incomes', element: <IncomesTransactions />},
            // {path: 'catalog/:id', element: <ProductDetails />},
            // {path: 'about', element: <AboutPage />},
            // {path: 'contact', element: <ContactPage />},
            // {path: 'server-error', element: <ServerError />},
            // {path: 'not-found', element: <NotFound />},
            // {path: 'basket', element: <BasketPage />},
            // {path: 'login', element: <Login />},
            // {path: 'register', element: <Register />},
            // {path: '*', element: <Navigate replace to='/not-found' />}
        ]
    }
])