var app = new Vue({
    el: '#app',
    data: {
        loading: false,
        products: [],
        objectIndex: 0,
        productViewModel: {
            id: "0",
            name: "ProductName",
            value: "1,10",
            description: "ProductDescription"
        }
    },
    mounted() {
        this.getProducts();
    },
    methods:{
        getProducts() {
            this.loading = true;
            axios.get('/Admin/products')
                .then(result => { console.log(result); this.products = result.data; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        getProduct(id) {
            this.loading = true;
            axios.get('/Admin/products/' + id)
                .then(result => {
                    console.log(result); var product = result.data;
                    this.productViewModel =
                    {
                        id: product.id,
                        description: product.description,
                        name: product.name,
                        value: product.value
                    };})
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/Admin/products/' + id)
                .then(result => { console.log(result); this.products.splice(index, 1); })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        createProduct() {
            this.loading = true;
            axios.post('/Admin/products', this.productViewModel)
                .then(result => { console.log(result); this.products = [...this.products, result.data] ; })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        updateProduct() {
            this.loading = true;
            axios.put('/Admin/products', this.productViewModel)
                .then(result => { console.log(result); this.products.splice(this.objectIndex, 1, result.data); })
                .catch(error => { console.log(error); })
                .then(() => { this.loading = false; });
        },
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);
        }
    }
});