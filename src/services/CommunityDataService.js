import http from "../http-common";
class CommunityDataService{
    getAll(){
        return http.get("/Community");
    }
    getCommunityById(id){
        return http.get(`/Community/${id}`);
    }
    UpdateCommunity(data) {
        return http.put(`/Community/`, data);
    }
}
export default new CommunityDataService();