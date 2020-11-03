var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        products:[]
    },
    methods:{
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(result => { console.log(result); this.products = result.data; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false });
        }
    }
});