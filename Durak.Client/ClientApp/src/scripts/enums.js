

export const LearFactory = (lear) => {
  return ['Spades','Diamonds','Hearts','Сlubs'][lear]
}
export const RankFactory = (rank) => {

  return ['R6','R7','R8','R9','R10','RJ','RQ','RK','RA'][rank-6]
};
export const RoleFactory = (role) => {
  return ['Attacker','Defender'][role]
  
};