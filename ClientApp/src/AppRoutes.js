import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { BookPage } from "./components/BookPage";


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    requireAuth: true,
    element: <FetchData />
    },
    {
        //route the BookPage component to the /book route
        path: '/book',
        requireAuth: true,
        element: <BookPage />

    },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
