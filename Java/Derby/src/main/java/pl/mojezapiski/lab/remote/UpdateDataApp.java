package pl.mojezapiski.lab.remote;

import java.sql.*;

public class UpdateDataApp {

    public UpdateDataApp() {
    }

    public void updateData() {
        Connection con = null;
        try {
            // Ładujemy plik z klasą sterownika bazy danych
            Class.forName("org.apache.derby.jdbc.ClientDriver");
            // Tworzymy połączenie do bazy danych
            con = DriverManager.getConnection("jdbc:derby://localhost:1527/lab", "lab", "lab");
            // Tworzymy obiekt wyrażenia
            Statement statement = con.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_UPDATABLE);
            // Wysyłamy zapytanie do bazy danych
            ResultSet rs = statement.executeQuery("SELECT * FROM Dane");
            // Przeglądamy otrzymane wyniki zamieniając wszystkie listery nazwiska na duże
            while (rs.next()) { 
                rs.updateString("nazwisko", rs.getString("nazwisko").toUpperCase() );
                rs.updateRow();
            }
            rs.absolute(3);
            rs.updateInt("ID", 3 );
            rs.updateRow();
            rs.close();
            System.out.println("Dane zauktualizowano.");
        } catch (SQLException sqle) {
            System.err.println("SQL exception: " + sqle.getMessage());
        } catch (ClassNotFoundException cnfe) {
            System.err.println("ClassNotFound exception: " + cnfe.getMessage());
        } catch (Exception e) {
            System.err.println("Another exception: " + e.getMessage());
        } finally {
            try {
                if (con != null) {
                    con.close();
                }
            } catch (SQLException sqle) {
                System.err.println("SQL exception: " + sqle.getMessage());
            }
        }
    }

    public static void main(String[] args) {
        UpdateDataApp updateDataApp = new UpdateDataApp();
        updateDataApp.updateData();
    }
}
