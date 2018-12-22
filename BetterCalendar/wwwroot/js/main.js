var app = new Vue({
    el: '#app',
    data: {
        modalOpened: false
    },

    methods: {

        openModal: function () {
            this.modalOpened = true
        },

        closeModal: function (event) {
            if (event.target.id == 'overlay' || event.target.id == 'close-modal-button') {
                this.modalOpened = false
            }
        }
    },

    mounted() {
        console.log("Mounted")
    }

})