
<template>

  <v-data-table dark :headers="headers" :items="community" :items-per-page="10" class="elevation-1">
    <template v-slot:[`item.actions`]="{ item }">
      <v-icon small class="mr-2" @click="editItem(item.id)">mdi-pencil</v-icon>
      <v-dialog v-model="dialog" max-width="500px">
                <v-card>
                  <v-card-title>
                    <span class="headline">Edit</span>
                  </v-card-title>
                  <v-card-text>
                    <v-container grid-list-md>
                      <v-layout wrap>
                        <v-flex xs12 sm6 md4>
                          <v-text-field v-model="editedItem.givenName" :rules="[(v) => !!v || 'given is required']" label="Given Name"></v-text-field>
                        </v-flex>
                        <v-flex xs12 sm6 md4>
                          <v-text-field v-model="editedItem.surname" :rules="[(v) => !!v || 'given is required']" label="SurName"></v-text-field>
                        </v-flex>
                        <v-flex xs12 sm6 md4>
                          <v-text-field v-model="editedItem.email" label="Email" :rules="emailRules"> </v-text-field>
                        </v-flex>
                        <v-flex xs12 sm6 md4>
                          <v-menu
          ref="menu"
          v-model="menu"
          :close-on-content-click="false"
          :return-value.sync="date"
          transition="scale-transition"
          offset-y
          min-width="auto"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              v-model="editedItem.birthDate"
              label="Picker in menu"
              prepend-icon="mdi-calendar"
              readonly
              v-bind="attrs"
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker
            v-model="editedItem.birthDate"
            no-title
            scrollable
          >
            <v-spacer></v-spacer>
            <v-btn
              text
              color="primary"
              @click="menu = false"
            >
              Cancel
            </v-btn>
            <v-btn
              text
              color="primary"
              @click="$refs.menu.save(date)"
            >
              OK
            </v-btn>
          </v-date-picker>
        </v-menu>
                          <!-- <template v-slot:activator="{ on }">
      <v-text-field
        v-model="item.date"
        label="Picker in dialog"
        prepend-icon="event"
        readonly
        v-on="on"
      ></v-text-field>
    </template>
    <v-date-picker v-model="editedItem.birthDate" scrollable>
      <div class="flex-grow-1"></div>
      <v-btn text color="primary" @click="modal = false">OK</v-btn>
    </v-date-picker> -->
                          <!-- <v-menu
            
            v-model="fromDateMenu"
            :close-on-content-click="false"
            :nudge-right="40"
            transition="scale-transition"
            offset-y 
            max-width="290px"
            min-width="290px"
          >
            <template v-slot:activator="{ on }">
              <v-text-field
                label="From"
                prepend-icon="event"
                readonly
                :value="fromDateDisp"
                v-on="on"
              ></v-text-field>
            </template>
            <v-date-picker
              locale="en-in"
              v-model="editedItem.birthDate"
              no-title
              @input="fromDateMenu = false"
              :min="minDate"
            ></v-date-picker>
            </v-menu> -->
                        </v-flex>
                       
                      </v-layout>
                    </v-container>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" flat @click.native="close">Cancel</v-btn>
                    <v-btn color="blue darken-1" flat @click.native="save">Save</v-btn>
                  </v-card-actions>
                </v-card>
</v-dialog>
    </template>
    
  </v-data-table>
</template>

<script>
import CommunityDataService from '@/services/CommunityDataService';
export default {
 
  data() {
    return {
      headers: [
        {
          text: 'Given Name',
          align: 'start',
          sortable: false,
          value: 'givenName',
        },
        { text: 'Surname', value: 'surname' },
        { text: 'Email', value: 'email' },
        { text: 'BirthDate', value: 'birthDate' },
        { text: "Actions", value: "actions", sortable: false },
      ],
      emailRules: [ 
        v => !v || /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) || 'E-mail must be valid'
      ],
      
    menu: false,
      community: [],
      dialog:false,
      editedIndex: -1,
      editedItem: {
        givenName: '',
        surname: '',
        email: '',
        birthDate:  (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)
        },
    };
  },
  methods: {
    
    retrieveCommunities() {
      CommunityDataService.getAll().then((response) => {
        this.community = response.data;
        console.log(response.data);
      })
        .catch((e) => {
          console.log(e);
        })
    },
    editItem(id) {
      console.log(id)
          this.editedIndex = id
          console.log(this.editedIndex)
          CommunityDataService.getCommunityById(id).then((response)=>{
            this.editedItem = Object.assign({}, response.data)
          })
          console.log(this.editedItem)
          this.dialog=true
        },
        close() {
          this.dialog = false
          setTimeout(() => {
            this.editedItem = Object.assign({}, this.defaultItem)
            this.editedIndex = -1
          }, 300)
        },

        save() {
          // if (this.editedIndex > -1) {
          //   this.listPrimitive.update(this.editedIndex, this.editedItem)
          // } else {
          //   this.listPrimitive.add(this.editedItem)
          // }
          console.log(this.editedItem)
          CommunityDataService.UpdateCommunity(this.editedIndex, Object.assign({}, this.editedItem))
          // .then((response)=>{
          //   this.listPrimitive.update(this.editedIndex, this.editedItem)
          // })

          this.close()
        }
  },
  
    mounted() {
      this.retrieveCommunities();
    },
    computed: {
      fromDateDisp() {
        return this.editedItem.birthDate;
        // format date, apply validations, etc. Example below.
        // return this.fromDateVal ? this.formatDate(this.fromDateVal) : "";
      },
    },
  };
</script>