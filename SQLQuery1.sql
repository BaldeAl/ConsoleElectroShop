CREATE VIEW V_CommandesAvecTotal AS 
SELECT C.Id, SUM(LC.Quantite * LC.PrixUnitaire) AS Total 
FROM Commandes C 
JOIN LignesCommandes LC ON C.Id = LC.CommandeId 
GROUP BY C.Id;
