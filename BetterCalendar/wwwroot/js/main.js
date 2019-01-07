var app = new Vue({
    el: '#app',
    data: {
        modalOpened: false,
        errors: [],
        title: "",
        start: new Date().getHours() + ":00",
        end: new Date().getHours() + 1 + ":00"
    },

    methods: {

        openModal: function () {
            this.modalOpened = true
            this.errors = []
        },

        closeModal: function (event) {
            if (event.target.id == 'overlay' || event.target.id == 'close-modal-button') {
                this.modalOpened = false
            }
        },

        checkForm: function () {
            this.errors = []

            if (this.title.length == 0) {
                this.errors.push("Tytuł jest wymagany")
            }

            if (this.validateHours() == false) {
                this.errors.push("Godzina rozpoczęcia nie moze byc pózniejsza niż godzina zakończenia")
            }

            if (this.errors.length == 0) {
                document.querySelector("#newEventForm").submit()
            }

            
        },

        validateHours: function () {

            if (this.end.length == 0) {
                return true
            }

            let startArr = this.start.split(":")
            let endArr = this.end.split(":")

            if (parseInt(startArr[0]) > parseInt(endArr[0])) {
                return false
            }

            if (parseInt(startArr[0]) == parseInt(endArr[0])) {
                if (parseInt(startArr[1]) > parseInt(endArr[1])) {
                    return false
                }

                return true
            }

            return true
        }
    },
})