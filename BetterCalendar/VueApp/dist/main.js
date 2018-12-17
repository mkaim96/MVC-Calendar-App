var app = new Vue({
    el: '#app',
    data: {
      events: []
    },

    methods: {
        logEvents: function() {
            console.log("clicked")
            console.log(this.events)
        }
    },

    mounted: function() {
        axios
            .get("http://localhost:51164/api/events/get-by-date/2018-11-21", { withCredentials: false  })
            .then(res => this.events = res)

    }
  })