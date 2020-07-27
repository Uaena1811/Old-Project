import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default {
    state: {
        common: {
            baseOrder: 'https://localhost:5001/',
            baseImage: 'https://localhost:5001/'

        },
        order: {
            list: 'Orders/Search'
        },
        image: {
            list: 'Images/Search'
        },
    },
    mutations: {

    },
    actions: {},
    getters: {
        productList: state => state.common.baseOrder + state.order.list,
        imageList: state => state.common.baseImage + state.image.list,

    },
}
