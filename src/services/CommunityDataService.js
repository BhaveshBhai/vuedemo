import http from "../http-common";
class CommunityDataService{
    getAll(){
        return http.get("/Community");
    }
    getCommunityById(id){
        return http.get(`/Community/${id}`);
    }
    UpdateCommunity(id,data) {
        return http.put(`/Community/${id}`, data);
    }
}
export default new CommunityDataService();