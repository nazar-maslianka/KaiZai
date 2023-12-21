import axios from 'axios';

// eslint-disable-next-line no-undef
axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const responseBody = (response) => response.data;

const requests = {
    get: (url) => axios.get(url).then(responseBody),
    post: (url, body) => axios.post(url, body).then(responseBody),
    put: (url, body) => axios.put(url, body).then(responseBody),
    del: (url) => axios.delete(url).then(responseBody)
};

const Incomes = {
    list: (params) => axios.get('/incomes', { params }).then(responseBody),
    details: (id) => requests.get(`/incomes/${id}`),
    create: (activity) => requests.post('/incomes', activity),
    update: (activity) => requests.put(`/incomes/${activity.id}`, activity),
    delete: (id) => requests.del(`/incomes/${id}`),
};

const agent = {
    Incomes
};

export default agent;